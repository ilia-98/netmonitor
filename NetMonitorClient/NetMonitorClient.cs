﻿using OpenHardwareMonitor.Hardware;
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

        static WebSocket socket;
        //tic WebSocket socketRemoteControl;
        static RemoteControl remoteControl;
        Thread monitorThread;
        
        
        public string Address { get; set; } = "192.168.1.10"; //"127.0.0.1";
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

        static List<Dictionary<string, object>> GetAppInfo()
        {
            List<Dictionary<string, object>> appList = new List<Dictionary<string, object>>();
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

                appList.Add(new Dictionary<string, object>() {
                    { "Name", app.GetPropertyValue("Name") },
                    { "InstallDate", app.GetPropertyValue("InstallDate") },
                    { "Vendor", app.GetPropertyValue("Vendor")} ,
                    { "Version", app.GetPropertyValue("Version") }
                });
            }
            return appList;
        }

        struct ProcessInfo
        {
            string Name;
            string CPUUsage;
            string RAMUsage;
            TimeSpan oldCPUTime;
        }

        static double[] GetUsage(Process process)
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
            double[] result = new double[2];
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
                result[0] = Math.Round(cpu.NextValue() / Environment.ProcessorCount, 2);
                // Returns number of MB consumed by application
                result[1] = Math.Round(ram.NextValue() / 1024 / 1024, 2);
            }
            catch
            {
                result[0] = 0;
                result[1] = 0;
            }
            return result;

        }

        static void GetProc()
        {
            Process[] processes;
            List<Dictionary<string, object>> procList = new List<Dictionary<string, object>>();
            while (true)
            {
                processes = Process.GetProcesses();
                foreach (var process in processes)
                {
                    try
                    {
                        double[] usage = GetUsage(process);
                        procList.Add(new Dictionary<string, object>() {
                            { "Name", process.ProcessName },
                            { "Description", process.MainModule.FileVersionInfo.FileDescription },
                            { "CPUSUsage", usage[0] },
                            { "RAMUsage", usage[1] }
                        });

                        //var cp = GetUsage(process)[0];
                        //if (cp > 0)
                        //{
                        //    Console.WriteLine();
                        //    Console.WriteLine(cp);
                        //    Console.WriteLine(process.MainModule.FileVersionInfo.FileDescription);
                        //    // Console.WriteLine(item.WorkingSet64 / 1024);
                        //    // Console.WriteLine(item.VirtualMemorySize64);
                        //    Console.WriteLine(process.ProcessName);
                        //}
                    }
                    catch { }
                }
            }
        }




        public void Start()
        {
            GetAppInfo();
            //GetProc();
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
                socket.Send(new Packet() { Header = "Hardware_Info", Data = MonitoringUtils.GetHardwareInfo() });
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
                    case "Hardware_Info":
                        {
                            socket.Send(new Packet() { Header = "Hardware_Info", Data = MonitoringUtils.GetHardwareInfo() });
                        }
                        break;
                    case "Monitor_Info":
                        {
                            socket.Send(new Packet() { Header = "Monitor_Info", Data = MonitoringUtils.GetMonitorInfo() });
                        }
                        break;
                    case "Screenshot":
                        {
                            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                            Graphics graphics = Graphics.FromImage(printscreen);
                            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
                            socket.Send(new Packet() { Header = "Screenshot", Data = printscreen });
                        }
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
                    case "GetAppInfo":
                        socket.Send(new Packet() { Header = "Monitor_Info", Data = GetAppInfo() });
                        break;
                    case "RemoteControl":
                        {
                            remoteControl = new RemoteControl(Address, Port);
                        }
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
