using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetMonitor;
using WebSocketSharp;

namespace NetMonitorClient
{
    public class RemoteControl
    {
        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SetCursorPos(uint x, uint y);


        public static bool Enabled { get; set; }
        public static WebSocket socketRemoteControl;
        public static Thread threadScreen;
        public RemoteControl( string address, int port)
        {
            socketRemoteControl = new WebSocket("ws://" + address + ":" + port + "/NetMonitorSocketService/RemoteControl");
            socketRemoteControl.OnClose += Socket_OnClose;
            socketRemoteControl.OnMessage += Socket_OnMessage;
            socketRemoteControl.OnOpen += Socket_OnOpen;
            socketRemoteControl.OnError += Socket_OnClose;
            socketRemoteControl.Connect();

        }

        public static void SendScreen()
        {
            while (true)
            {
                try {
                    Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                    Graphics graphics = Graphics.FromImage(printscreen);
                    graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
                    socketRemoteControl.Send(new Packet() {
                        Header = "RemoteControl/Screen", Data = printscreen
                    });

                }
                catch { socketRemoteControl.Close(); }
                }
        }

        public static void pressLeftMouse(Packet packet)
        {
            MouseEvent mouseEvent = (MouseEvent)packet.Data;
            SetCursorPos(mouseEvent.X, mouseEvent.Y);
            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }

        public static void moveMouse(Packet packet)
        {
            MouseEvent mouseEvent = (MouseEvent)packet.Data;
            SetCursorPos(mouseEvent.X, mouseEvent.Y);
        }

        private static void Socket_OnClose(object sender, CloseEventArgs e)
        {
            //if (Enabled)
            //{
            //    Thread.Sleep(3000);
            //    socketRemoteControl.ConnectAsync();
            //}
            socketRemoteControl.Close();
        }

        private static void Socket_OnOpen(object sender, EventArgs e)
        {
            try
            {
                while(socketRemoteControl == null) { }
                socketRemoteControl.Send(Packet.Serialize(new Packet()
                {
                    Header = "RemoteControl/Resolution",
                    Data = new Dictionary<string, int>() {
                    { "Width" ,Screen.PrimaryScreen.Bounds.Width },
                    { "Height" ,Screen.PrimaryScreen.Bounds.Height },
                }
                }));
            }
            catch { }
        }

        private static void Socket_OnClose(object sender, EventArgs e)
        {
            socketRemoteControl.Close();
        }


        private static void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                Packet packet = e.RawData;
                switch (packet.Header)
                {
                    case "RemoteControl/Start":
                        {
                            threadScreen = new Thread(() => SendScreen());
                            threadScreen.Start();
                        }
                        break;
                    case "MouseEvent/Click":
                        RemoteControl.pressLeftMouse(packet);
                        break;
                    case "MouseEvent/Move":
                        RemoteControl.moveMouse(packet);
                        break;
                    case "RemoteControl/Stop":
                        threadScreen.Abort();
                        break;
                    default:
                        {
                            Console.WriteLine("Ошибка");
                        }
                        break;
                }
            }
            catch { }
        }

    }
}
