using NetMonitor;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WebSocketSharp;

namespace NetMonitorClient
{
    class NetMonitorClient
    {
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,
        }


        static WebSocket socket;
        static RemoteControl remoteControl;
        Thread monitorThread;
        Thread processThread;

        public string[] Addresses { get; } = { "127.0.0.1", "192.168.43.9", "192.168.1.10" };
        public string Address { get; set; } = "192.168.43.9";
        public static NotifyIcon NotifyIcon { get; set; }
        public static int Port { get; set; } = 1348;
        public static bool Enabled { get; set; } = true;
        public static bool NoticeEnabled { get; set; } = false;
        public static string EmailTo { get; set; }
        public static int CriticalTemperature { get; set; } = 80;

        public NetMonitorClient(NotifyIcon notifyIcon)
        {
            NotifyIcon = notifyIcon;
        }

        struct ProcessInfo
        {
            string Name;
            string CPUUsage;
            string RAMUsage;
            TimeSpan oldCPUTime;
        }

        public void Start()
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd", @"lodctr/r");
            Process.Start(psi);

            socket = new WebSocket("ws://" + Address + ":" + Port + "/NetMonitorSocketService/Main");
            MonitoringUtils.UpdateSensors();
            socket.OnClose += Socket_OnClose;
            socket.OnMessage += Socket_OnMessage;
            socket.OnOpen += Socket_OnOpen;
            socket.Connect();

            monitorThread = new Thread(() => MonitoringUtils.Monitoring(socket, CriticalTemperature));
            monitorThread.Start();
            processThread = new Thread(() => MonitoringUtils.GetProcesses());
            processThread.Start();
        }

        public void Stop()
        {
            Enabled = false;
            socket.Close();
            monitorThread.Abort();
            processThread.Abort();
        }


        private static void Socket_OnOpen(object sender, EventArgs e)
        {
            try
            {
                NotifyIcon.Icon = Properties.Resources.Ok;
                socket.Send(new Packet() { Header = "HardwareInfo", Data = MonitoringUtils.GetHardwareInfo() });
            }
            catch { }
        }


        private void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                Packet packet = e.RawData;
                switch (packet.Header)
                {
                    case "HardwareInfo":
                        socket.Send(new Packet() { Header = "HardwareInfo", Data = MonitoringUtils.GetHardwareInfo() });
                        break;
                    case "MonitorInfo":
                        socket.Send(new Packet() { Header = "MonitorInfo", Data = MonitoringUtils.GetMonitorInfo() });
                        break;
                    case "ShowBalloonTip":
                        {
                            try
                            {
                                BalloonTip balloonTip = (BalloonTip)packet.Data;
                                NotifyIcon.ShowBalloonTip(balloonTip.timeout, balloonTip.tipTitle, balloonTip.tipText, balloonTip.tipIcon);
                                socket.Send(new Packet() { Header = "ShowBalloonTip/Result", Data = "OK" });
                            }
                            catch(Exception exp)
                            {
                                socket.Send(new Packet() { Header = "ShowBalloonTip/Result", Data = exp.Message, IsError = true });
                            }
                        }
                        break;
                    case "Screenshot":
                        //Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                        //Graphics graphics = Graphics.FromImage(printscreen);
                        //graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
                        socket.Send(new Packet() { Header = "Screenshot", Data = RemoteControl.GetScreenshot() });
                        break;
                    case "ProcessInfo":
                        socket.Send(new Packet() { Header = "ProcessInfo", Data = MonitoringUtils.processesList });
                        break;
                    case "ApplicationInfo":
                        socket.Send(new Packet() { Header = "ApplicationInfo", Data = MonitoringUtils.GetAppInfo() });
                        break;
                    case "OpenPorts":
                        socket.Send(new Packet() { Header = "OpenPorts", Data = MonitoringUtils.GetOpenPorts() });
                        break;
                    case "TcpConnections":
                        socket.Send(new Packet() { Header = "TcpConnections", Data = MonitoringUtils.GetTcpConnections() });
                        break;
                    case "Files/GetUpdate":
                        FileUtils.SendFilesUpdate(socket);
                        break;
                    case "Files/GetElementsFromPath":
                        FileUtils.SendElementsFromPath(socket, packet);
                        break;
                    case "Files/RunFileOnPath":
                        FileUtils.RunFileOnPath(socket, packet);
                        break;
                    case "Files/UploadFile":
                        FileUtils.UploadFile(socket, packet);
                        break;
                    case "Files/GetFile":
                        FileUtils.GetFile(socket, packet);
                        break;
                    case "Files/DeleteFile":
                        FileUtils.DeleteFile(socket, packet);
                        break;
                    case "RemoteControl":
                        remoteControl = new RemoteControl(Address, Port);
                        break;
                    default:
                        {
                            socket.Send(new Packet() { Header = "Error", Data = packet });
                            Console.WriteLine("Ошибка");
                        }
                        break;
                }
            }
            catch { }
        }

        private static void Socket_OnClose(object sender, CloseEventArgs e)
        {
            NotifyIcon.Icon = Properties.Resources.Error;
            if (Enabled)
            {
                Thread.Sleep(3000);
                socket.ConnectAsync();
            }
        }
    }
}
