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

namespace NetMonitorClient
{
    class NetMonitorClient
    {
        static Computer computerHardware;
        static WebSocket socket;
        static int CPU_index;
        static int RAM_index;
        static int HDD_index;
        static int RAMLOADsensor_index;
        static int CPUTEMPsensor_index;
        static int CPULOADsensor_index;
        static List<int> HDDTEMPsensors_index = new List<int>();

        public string Address { get; set; } = "127.0.0.1";
        public static NotifyIcon NotifyIcon { get; set; }
        public static int Port { get; set; } = 1348;
        public static bool Enabled { get; set; } = true;

        public NetMonitorClient(NotifyIcon notifyIcon)
        {
            NotifyIcon = notifyIcon;
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
            socket = new WebSocket("ws://" + Address + ":" + Port + "/NetMonitorSocketService");
            UpdateSensors();
            socket.OnClose += Socket_OnClose;
            socket.OnMessage += Socket_OnMessage;
            socket.OnOpen += Socket_OnOpen;
            socket.Connect();
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
                if (e.IsText)
                {
                    if (e.Data == "Monitor_Info")
                    {
                        socket.Send(Packet.Serialize(new Packet() { Header = "Monitor_Info", Data = GetMonitorInfo() }));
                    }

                    if (e.Data == "Screenshot")
                    {
                        Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                        Graphics graphics = Graphics.FromImage(printscreen);
                        graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
                        socket.Send(Packet.Serialize(new Packet() { Header = "Screenshot", Data = printscreen }));
                    }
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
