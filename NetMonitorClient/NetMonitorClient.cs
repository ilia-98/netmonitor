using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;
using NetMonitor;
using System.Windows.Forms;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.InteropServices;

namespace NetMonitorClient
{
    class NetMonitorClient
    {
        static Computer computerHardware;
        static WebSocket socket;
        static SmtpClient smtpClient;
        Thread monitorThread;
        static int CPU_index;
        static int RAM_index;
        static int HDD_index;
        static int RAMLOADsensor_index;
        static int CPUTEMPsensor_index;
        static int CPULOADsensor_index;
        static List<int> HDDTEMPsensors_index = new List<int>();

        public string Address { get; set; } = "192.168.43.9";//"127.0.0.1";
        public static NotifyIcon NotifyIcon { get; set; }
        public static int Port { get; set; } = 1348;
        public static bool Enabled { get; set; } = true;
        public static bool NoticeEnabled { get; set; } = false;
        public static string EmailTo { get; set; }
        public static int CriticalTemperature { get; set; } = 80;

        public NetMonitorClient(NotifyIcon notifyIcon)
        {
            NotifyIcon = notifyIcon;
            smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("netmonitor.client@gmail.com", "1w3r5y7UIO");
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        public void UpdateSensors()
        {
            HDDTEMPsensors_index.Clear();
            computerHardware = new Computer();
            computerHardware.CPUEnabled = true;
            computerHardware.RAMEnabled = true;
            computerHardware.HDDEnabled = true;
            computerHardware.Open();
            int sensorcount;
            int hardwareCount = computerHardware.Hardware.Count();
            for (int i = 0; i < hardwareCount; i++)
            {
                computerHardware.Hardware[i].Update();

                if (computerHardware.Hardware[i].HardwareType == HardwareType.CPU)
                {
                    CPU_index = i;
                }
                if (computerHardware.Hardware[i].HardwareType == HardwareType.RAM)
                {
                    RAM_index = i;
                }
                if (computerHardware.Hardware[i].HardwareType == HardwareType.HDD)
                {
                    HDD_index = i;
                }

                sensorcount = computerHardware.Hardware[i].Sensors.Count();

                if (sensorcount > 0)
                {
                    for (int j = 0; j < sensorcount; j++)
                    {
                        if (computerHardware.Hardware[i].Sensors[j].SensorType == SensorType.Temperature
                        && computerHardware.Hardware[i].HardwareType == HardwareType.CPU)
                        {
                            CPUTEMPsensor_index = j;
                        }

                        if (computerHardware.Hardware[i].Sensors[j].SensorType == SensorType.Load
                        && computerHardware.Hardware[i].HardwareType == HardwareType.CPU)
                        {
                            CPULOADsensor_index = j;
                        }

                        if (computerHardware.Hardware[i].Sensors[j].Name == "Used Memory"
                        && computerHardware.Hardware[i].HardwareType == HardwareType.RAM)
                        {
                            RAMLOADsensor_index = j;
                        }

                        if (computerHardware.Hardware[i].Sensors[j].SensorType == SensorType.Temperature
                       && computerHardware.Hardware[i].HardwareType == HardwareType.HDD)
                        {
                            HDDTEMPsensors_index.Add(j);
                        }
                    }
                }
            }
        }


        static HashSet<int> GetOpenPorts()
        {
            IPEndPoint[] ports = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
            HashSet<int> p_hashset = new HashSet<int>();
            foreach (var item in ports)
            {
                p_hashset.Add(item.Port);
            }
            return p_hashset;
        }

        static HashSet<string> GetTcpConnections()
        {
            TcpConnectionInformation[] tcp_connections = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections();
            HashSet<string> tc_hashset = new HashSet<string>();
            foreach (var item in tcp_connections)
            {
                tc_hashset.Add(item.RemoteEndPoint.ToString());
            }
            return tc_hashset;
        }

        static void GetAppInfo()
        {
            var disks = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_Product").Get().OfType<ManagementObject>()
                         select x);
            string Q = "SELECT * FROM Win32_Product";
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(Q);
            foreach (ManagementObject obj in searcher.Get())
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("Name", obj.GetPropertyValue("Name"));
                dict.Add("InstallDate", obj.GetPropertyValue("InstallDate"));
                dict.Add("InstallLocation", obj.GetPropertyValue("InstallLocation"));
                dict.Add("Vendor", obj.GetPropertyValue("Vendor"));
                dict.Add("Version", obj.GetPropertyValue("Version"));
                Console.WriteLine("\n");
                foreach (var a in dict)
                {
                    Console.WriteLine(a.Key + " - " + a.Value);
                }
                result.Add(dict);
            }
            Console.Write(1);
        }

        struct ProcessInfo
        {
            string Name;
            string CPUUsage;
            string RAMUsage;
            TimeSpan oldCPUTime;
        }

        struct Cpu
        {
            public double cpu;
            public double ram;
        }

        static Cpu GetUsage(Process process)
        {
            // Getting information about current process
            // Preparing variable for application instance name
            string name = "";

            foreach (var instance in new PerformanceCounterCategory("Process").GetInstanceNames())
            {
                if (instance.StartsWith(process.ProcessName))
                {
                    using (var processId = new PerformanceCounter("Process", "ID Process", instance, true))
                    {
                        if (process.Id == (int)processId.RawValue)
                        {
                            name = instance;
                            break;
                        }
                    }
                }
            }
            Cpu result = new Cpu();
            try
            {
                var cpu = new PerformanceCounter("Process", "% Processor Time", name, true);
                var ram = new PerformanceCounter("Process", "Private Bytes", name, true);

                // Getting first initial values
                cpu.NextValue();
                ram.NextValue();

                // Creating delay to get correct values of CPU usage during next query
                Thread.Sleep(500);

                // If system has multiple cores, that should be taken into account
                result.cpu = Math.Round(cpu.NextValue() / Environment.ProcessorCount, 2);
                // Returns number of MB consumed by application
                result.ram = Math.Round(ram.NextValue() / 1024 / 1024, 2);
            }
            catch
            {
                result.cpu = 0;
                result.ram = 0;
            }
            return result;

        }

        static void GetProc()
        {
            Process[] procList;
            Dictionary<string, object> procdict = new Dictionary<string, object>();
            while (true)
            {
                procList = Process.GetProcesses();
                foreach (var item in procList)
                {
                    try
                    {
                        var cp = GetUsage(item).cpu;
                        if (cp > 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine(cp);
                            Console.WriteLine(item.MainModule.FileVersionInfo.FileDescription);
                            Console.WriteLine(item.WorkingSet64 / 1024);
                            Console.WriteLine(item.VirtualMemorySize64);
                            Console.WriteLine(item.ProcessName);
                        }
                    }
                    catch { }
                }
            }
        }

        static Dictionary<string, string> GetHardwareInfo()
        {
            Dictionary<string, string> hardwareInfo = new Dictionary<string, string>();

            string manufacturer = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard").Get().OfType<ManagementObject>()
                                   select x.GetPropertyValue("Manufacturer")).First().ToString();
            string os_name = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
                              select x.GetPropertyValue("Caption")).First().ToString();
            string cpu_name = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_Processor").Get().OfType<ManagementObject>()
                               select x.GetPropertyValue("Name")).First().ToString();
            string disks = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk").Get().OfType<ManagementObject>()
                            select x.GetPropertyValue("DeviceID")).Aggregate((av, e) => (string)av + " " + (string)e).ToString();
            string ram = ((ulong)(from x in new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory").Get().OfType<ManagementObject>()
                                  select x.GetPropertyValue("Capacity")).Aggregate((av, e) => (ulong)av + (ulong)e) / (1024 * 1024 * 1024)).ToString();
            string video = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_VideoController").Get().OfType<ManagementObject>()
                            select x.GetPropertyValue("Caption")).Aggregate((av, e) => "\n\t" + (string)av + "\n\t" + (string)e).ToString();

            hardwareInfo.Add("Производитель", manufacturer);
            hardwareInfo.Add("Название компьютера", Environment.MachineName);
            hardwareInfo.Add("Имя пользователя", Environment.UserName);
            hardwareInfo.Add("Операционная система", os_name);
            hardwareInfo.Add("64-разрядная ОС", Environment.Is64BitOperatingSystem ? "Да" : "Нет");
            hardwareInfo.Add("Разрешение экрана", Screen.PrimaryScreen.Bounds.Width + "x" + Screen.PrimaryScreen.Bounds.Height);
            hardwareInfo.Add("Процессор", cpu_name);
            hardwareInfo.Add("Логические диски", disks);
            hardwareInfo.Add("ОЗУ", ram + " Гб");
            hardwareInfo.Add("Видеоадаптеры", video);

            return hardwareInfo;
        }

        static MonitorInfo GetMonitorInfo()
        {
            MonitorInfo monitorInfo = new MonitorInfo()
            {
                HDD_temp = new float[HDDTEMPsensors_index.Count],
                CPU_load = 0,
                CPU_temp = 0,
                RAM_load = 0
            };

            computerHardware.Hardware[CPU_index].Update();
            computerHardware.Hardware[HDD_index].Update();
            computerHardware.Hardware[RAM_index].Update();

            float? value;
            for (int i = 0; i < HDDTEMPsensors_index.Count; i++)
            {
                value = computerHardware.Hardware[HDD_index].Sensors[HDDTEMPsensors_index[i]].Value;
                if (value != null)
                    monitorInfo.HDD_temp[i] = (float)value;
            }
            value = computerHardware.Hardware[CPU_index].Sensors[CPUTEMPsensor_index].Value;
            if (value != null)
                monitorInfo.CPU_temp = (float)value;
            value = computerHardware.Hardware[CPU_index].Sensors[CPULOADsensor_index].Value;
            if (value != null)
                monitorInfo.CPU_load = (float)value;
            value = computerHardware.Hardware[RAM_index].Sensors[RAMLOADsensor_index].Value;
            if (value != null)
                monitorInfo.RAM_load = (float)value;

            return monitorInfo;
        }


        public void Start()
        {
            //GetProc();
            socket = new WebSocket("ws://" + Address + ":" + Port + "/NetMonitorSocketService");
            UpdateSensors();
            socket.OnClose += Socket_OnClose;
            socket.OnMessage += Socket_OnMessage;
            socket.OnOpen += Socket_OnOpen;
            socket.Connect();
            monitorThread = new Thread(Monitoring);
            monitorThread.Start();
        }

        public void Monitoring()
        {
            MonitorInfo info;
            while (true)
            {
                info = GetMonitorInfo();
                if (info.CPU_temp > CriticalTemperature)
                {
                    try
                    {
                        smtpClient.Send(new MailMessage("netmonitor.client@gmail.com", EmailTo, "Warning", "Температура процессора превысила " + CriticalTemperature));
                    }
                    catch { }
                }
                Thread.Sleep(10000);
            }
        }

        public void Stop()
        {
            Enabled = false;
            socket.Close();
        }


        private static void Socket_OnOpen(object sender, EventArgs e)
        {
            try
            {
                NotifyIcon.Icon = Properties.Resources.Ok;
                socket.Send(Packet.Serialize(new Packet() { Header = "Hardware_Info", Data = GetHardwareInfo() }));
            }
            catch { }
        }


        private static void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                Packet packet = e.RawData;
                switch (packet.Header)
                {
                    case "Hardware_Info":
                        {
                            socket.Send(Packet.Serialize(new Packet() { Header = "Hardware_Info", Data = GetHardwareInfo() }));
                        }
                        break;
                    case "Monitor_Info":
                        {
                            socket.Send(Packet.Serialize(new Packet() { Header = "Monitor_Info", Data = GetMonitorInfo() }));
                        }
                        break;
                    case "Screenshot":
                        {
                            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                            Graphics graphics = Graphics.FromImage(printscreen);
                            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
                            socket.Send(Packet.Serialize(new Packet() { Header = "Screenshot", Data = printscreen }));
                        }
                        break;
                    case "Files/GetUpdate":
                        FileUtils.ОтправитьFilesUpdate(socket);
                        break;
                    case "Files/GetElementsFromPath":
                        FileUtils.ОтправитьElementsFromPath(socket, packet);
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
                    case "MouseEvent/Click":
                        RemoteControl.pressLeftMouse(packet);
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
