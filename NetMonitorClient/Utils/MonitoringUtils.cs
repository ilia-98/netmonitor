using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetMonitor;
using OpenHardwareMonitor.Hardware;
using WebSocketSharp;

namespace NetMonitorClient
{
    static public class MonitoringUtils
    {
        static Computer computerHardware;
        static int CPU_index;
        static int RAM_index;
        static List<int> HDD_index = new List<int>();
        static int RAMLOADsensor_index;
        static int CPUTEMPsensor_index;
        static int CPULOADsensor_index;
        static int HDDTEMPsensor_index;
        public static List<Dictionary<string, object>> processesList = new List<Dictionary<string, object>>();

        public static void UpdateSensors()
        {
            HDD_index.Clear();
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
                    HDD_index.Add(i);
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
                            HDDTEMPsensor_index = j;                        
                        }
                    }
                }
            }
        }

        public static void Monitoring(WebSocket socket,float CriticalTemperature = 80)
        {
            MonitorInfo info;
            while (true)
            {
                info = GetMonitorInfo();
                if (info.CPU_temp > CriticalTemperature)
                {
                    try
                    {
                        socket.Send(new Packet() { Header = "Notify/CriticalTemp" , Data = "Температура процессора превысила " + CriticalTemperature });
                       // smtpClient.Send(new MailMessage("netmonitor.client@gmail.com", EmailTo, "Warning", "Температура процессора превысила " + CriticalTemperature));
                    }
                    catch { }
                }
                Thread.Sleep(10000);
            }
        }

        public static Dictionary<string, string> GetHardwareInfo()
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

        public static MonitorInfo GetMonitorInfo()
        {
            MonitorInfo monitorInfo = new MonitorInfo()
            {
                HDD_temp = new float[HDD_index.Count],
                CPU_load = 0,
                CPU_temp = 0,
                RAM_load = 0
            };

            computerHardware.Hardware[CPU_index].Update();           
            computerHardware.Hardware[RAM_index].Update();

            float? value;
            for (int i = 0; i < HDD_index.Count; i++)
            {
                computerHardware.Hardware[HDD_index[i]].Update();
                value = computerHardware.Hardware[HDD_index[i]].Sensors[HDDTEMPsensor_index].Value;
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


        static double[] GetUsage(Process process)
        {
            string name = "";
            double[] result = new double[2];
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

            try
            {
                var cpu = new PerformanceCounter("Process", "% Processor Time", name, true);
                var ram = new PerformanceCounter("Process", "Private Bytes", name, true);

                cpu.NextValue();
                ram.NextValue();

                Thread.Sleep(250);

                result[0] = Math.Round(cpu.NextValue() / Environment.ProcessorCount, 2);
                result[1] = Math.Round(ram.NextValue() / 1024 / 1024, 2);
            }
            catch
            {
                result[0] = 0;
                result[1] = 0;
            }
            return result;

        }

        public static void GetProcesses()
        {
            while (true)
            {
                Console.WriteLine("Старт");
                Process[] processes;
                List<Dictionary<string, object>> temp = new List<Dictionary<string, object>>();

                processes = Process.GetProcesses();
                foreach (var process in processes)
                {
                    try
                    {
                        double[] usage = GetUsage(process);
                        temp.Add(new Dictionary<string, object>() {
                            { "Name", process.ProcessName },
                            { "Description", process.MainModule.FileVersionInfo.FileDescription },
                            { "CPUUsage", usage[0] },
                            { "RAMUsage", usage[1] }
                        });
                    }
                    catch { }
                }
                processesList = temp;
                Console.WriteLine("Стоп" + temp.Count);
            }
        }

        public static List<ApplicationInfo> GetAppInfo()
        {
            List<ApplicationInfo> appList = new List<ApplicationInfo>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
            foreach (ManagementObject app in searcher.Get())
            {
                //string appName, appInstDate, appInstLoc, appVendor, appVersion;
                //if (app.GetPropertyValue("Name") != null)
                //    appName = app.GetPropertyValue("Name").ToString();
                //else
                //    appName = "";
                //if (app.GetPropertyValue("InstallDate") != null)
                //    appInstDate = app.GetPropertyValue("InstallDate").ToString();
                //else
                //    appInstDate = "";
                //if (app.GetPropertyValue("Vendor") != null)
                //    appVendor = app.GetPropertyValue("Vendor").ToString();
                //else
                //    appVendor = "";
                //if (app.GetPropertyValue("Version") != null)
                //    appVersion = app.GetPropertyValue("Version").ToString();
                //else
                //    appVersion = "";

                var app_struct = new ApplicationInfo() {
                    Name = app.GetPropertyValue("Name") == null ? "" :app.GetPropertyValue("Name").ToString(),
                    InstallDate = app.GetPropertyValue("InstallDate") == null ? "" : app.GetPropertyValue("InstallDate").ToString(),
                    Vendor = app.GetPropertyValue("Vendor") == null ? "" : app.GetPropertyValue("Vendor").ToString(),
                    Version = app.GetPropertyValue("Version") == null ? "" : app.GetPropertyValue("Version").ToString(),
                };

                appList.Add(app_struct);
            }
            return appList;
        }


        public static HashSet<int> GetOpenPorts()
        {
            IPEndPoint[] ports = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
            HashSet<int> ports_hashset = new HashSet<int>();
            foreach (var item in ports)
            {
                ports_hashset.Add(item.Port);
            }
            return ports_hashset;
        }

        public static HashSet<string> GetTcpConnections()
        {
            TcpConnectionInformation[] tcp_connections = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections();
            HashSet<string> tcp_hashset = new HashSet<string>();
            foreach (var item in tcp_connections)
            {
                tcp_hashset.Add(item.RemoteEndPoint.ToString());
            }
            return tcp_hashset;
        }

    }
}
