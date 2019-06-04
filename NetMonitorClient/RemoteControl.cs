using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,
        }

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


        public static bool Enabled { get; set; } = true;
        public static WebSocket socketRemoteControl;
        public static Thread threadScreen;
        public static Rectangle screenResolution;

        public static string Address = null;

        public RemoteControl(string address, int port)
        {
            Address = address;

            socketRemoteControl = new WebSocket("ws://" + address + ":" + port + "/NetMonitorSocketService/RemoteControl");
            socketRemoteControl.OnClose += Socket_OnClose;
            socketRemoteControl.OnMessage += Socket_OnMessage;
            socketRemoteControl.OnOpen += Socket_OnOpen;
            socketRemoteControl.OnError += Socket_OnClose;
            socketRemoteControl.Connect();
        }


        private static float GetScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);
            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;
            return ScreenScalingFactor;
        }

        public static Rectangle GetScreenResolution()
        {
            float scale = GetScalingFactor();
            return new Rectangle(0, 0, (int)(Screen.PrimaryScreen.Bounds.Width * scale), (int)(Screen.PrimaryScreen.Bounds.Height * scale));
        }

        public static Bitmap GetScreenshot()
        {
            Bitmap bmp = new Bitmap(GetScreenResolution().Width, GetScreenResolution().Height);
            Graphics graph = Graphics.FromImage(bmp);
            graph.CopyFromScreen(0, 0, 0, 0, bmp.Size);
            return bmp;
        }

        public static Bitmap GetScreen()
        {
            Bitmap bmp = new Bitmap(screenResolution.Width, screenResolution.Height);
            Graphics graph = Graphics.FromImage(bmp);
            graph.CopyFromScreen(0, 0, 0, 0, bmp.Size);
            return bmp;
        }

        public static void SendScreen()
        {

            TcpClient udpClient = new TcpClient();
            udpClient.Connect(Address, 1350);
            NetworkStream netStream = udpClient.GetStream();
            try
            {
                while (Enabled)
                {

                    //Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                    //Graphics graphics = Graphics.FromImage(printscreen);
                    //graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

                    MemoryStream ms = new MemoryStream();
                    GetScreen().Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] bmpBytes = ms.ToArray();
                    ms.Close();

                    netStream.Write(BitConverter.GetBytes(bmpBytes.Length), 0, BitConverter.GetBytes(bmpBytes.Length).Length);
                    netStream.Write(bmpBytes, 0, bmpBytes.Length);

                    //socketRemoteControl.Send(new Packet() { Header = "RemoteControl/Screen", Data = GetScreen() });

                    //socketRemoteControl.Close();

                }
            }
            catch { }

        }

        public static void PressLeftMouse(Packet packet)
        {
            MouseEvent mouseEvent = (MouseEvent)packet.Data;
            SetCursorPos(mouseEvent.X, mouseEvent.Y);
            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }

        public static void MoveMouse(Packet packet)
        {
            MouseEvent mouseEvent = (MouseEvent)packet.Data;
            SetCursorPos(mouseEvent.X, mouseEvent.Y);
        }

        private static void Socket_OnClose(object sender, CloseEventArgs e)
        {
            Enabled = false;
            threadScreen.Abort();
        }

        private static void Socket_OnOpen(object sender, EventArgs e)
        {
            try
            {
                while (socketRemoteControl == null) { }
                socketRemoteControl.Send(Packet.Serialize(new Packet()
                {
                    Header = "RemoteControl/Resolution",
                    Data = new Dictionary<string, int>() {
                    { "Width", GetScreenResolution().Width },
                    { "Height", GetScreenResolution().Height },
                }
                }));
                screenResolution = GetScreenResolution();
            }
            catch { }
        }

        private static void Socket_OnClose(object sender, EventArgs e)
        {
            if (Enabled)
            {
                Thread.Sleep(3000);
                threadScreen.Abort();
                socketRemoteControl.Close();
            }
            Enabled = false;
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
                            Enabled = true;
                            threadScreen = new Thread(() => SendScreen());
                            threadScreen.Start();
                        }
                        break;
                    case "MouseEvent/Click":
                        PressLeftMouse(packet);
                        break;
                    case "MouseEvent/Move":
                        MoveMouse(packet);
                        break;
                    case "RemoteControl/Stop":
                        {
                            Enabled = false;
                            threadScreen.Abort();
                        }
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
