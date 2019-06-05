using MongoDB.Bson;
using MongoDB.Driver;
using NetMonitor;
using NetMonitorServer.Addons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace NetMonitorServer
{

    public class NetMonitorClient : WebSocketBehavior
    {
        string ClientIP { get; set; }
        string ClientMAC { get { return client.MAC; } }
        string EmailTo { get; set; }
        SmtpClient smtpClient;
        FormMain form;
        Client client;

        public NetMonitorClient(FormMain value, SmtpClient smtp, string email)
        {
            form = value;
            smtpClient = smtp;
            EmailTo = email;
        }

        void NewConnection(NetMonitorClient netMonitorClient)
        {
            form.listViewClients.BeginInvoke((MethodInvoker)(delegate
            {
                bool addtolist = true;

                string MAC_addr = Util.GetMacAddress(netMonitorClient.Context.UserEndPoint.Address.ToString()).ToUpper();

                foreach (Client item in form.listViewClients.Items)
                {
                    if (item.MAC == MAC_addr)
                    {
                        item.Available = true;
                        item.socket = this;
                        addtolist = false;
                        client = item;
                        break;
                    }
                }

                if (addtolist)
                {
                    client = new Client(netMonitorClient);
                    client.Available = true;
                    form.listViewClients.Items.Add(client);
                }

                if (form.StatusDBServer)
                {
                    ClientDB clientDB = client;
                    form.clientDBCollection.UpdateOne(clientDB.GetFilter(), clientDB.GetUpdate(), new UpdateOptions() { IsUpsert = true });
                }
            }));
        }

        void CloseConnection()
        {
            form.listViewClients.BeginInvoke((MethodInvoker)(delegate
            {
                foreach (Client item in form.listViewClients.Items)
                    if (item.MAC == client.MAC)
                    {
                        item.Available = false;
                    }
            }));
        }

        void Handler_Monitor_Info(Packet packet)
        {
            MonitorInfo info = (MonitorInfo)packet.Data;
            form.chartMonitor.BeginInvoke((MethodInvoker)(delegate
            {
                Series[] seriesHDD = new Series[info.HDD_temp.Length];
                Series seriesCPUTemp = new Series();
                Series seriesCPULoad = new Series();
                Series seriesRAMLoad = new Series();
                for (int i = 0; i < info.HDD_temp.Length; i++)
                {
                    seriesHDD[i] = form.chartMonitor.Series.FindByName("HDD" + (i + 1) + " °C");
                    if (seriesHDD[i] == null)
                    {
                        seriesHDD[i] = new Series("HDD" + (i + 1) + " °C");
                        seriesHDD[i].ChartType = SeriesChartType.Line;
                        seriesHDD[i].ChartArea = "TempMonitoring";
                        seriesHDD[i].BorderWidth = 3;
                        form.chartMonitor.Series.Add(seriesHDD[i]);
                    }
                    seriesHDD[i].Points.AddXY(DateTime.Now.ToLongTimeString(), info.HDD_temp[i]);
                    if (seriesHDD[i].Points.Count > 6)
                        seriesHDD[i].Points.RemoveAt(0);
                }

                seriesCPUTemp = form.chartMonitor.Series.FindByName("CPU °C");
                if (seriesCPUTemp == null)
                {
                    seriesCPUTemp = new Series("CPU °C");
                    seriesCPUTemp.ChartType = SeriesChartType.Line;
                    seriesCPUTemp.ChartArea = "TempMonitoring";
                    seriesCPUTemp.BorderWidth = 3;
                    form.chartMonitor.Series.Add(seriesCPUTemp);
                }
                seriesCPUTemp.Points.AddXY(DateTime.Now.ToLongTimeString(), info.CPU_temp);
                if (seriesCPUTemp.Points.Count > 6)
                    seriesCPUTemp.Points.RemoveAt(0);

                seriesCPULoad = form.chartMonitor.Series.FindByName("CPU %");
                if (seriesCPULoad == null)
                {
                    seriesCPULoad = new Series("CPU %");
                    seriesCPULoad.ChartType = SeriesChartType.Column;
                    seriesCPULoad.ChartArea = "LoadMonitoring";

                    seriesCPULoad.BorderWidth = 3;
                    form.chartMonitor.Series.Add(seriesCPULoad);
                }
                seriesCPULoad.Points.AddXY(DateTime.Now.ToLongTimeString(), info.CPU_load);
                if (seriesCPULoad.Points.Count > 1)
                    seriesCPULoad.Points.RemoveAt(0);

                seriesRAMLoad = form.chartMonitor.Series.FindByName("RAM %");
                if (seriesRAMLoad == null)
                {
                    seriesRAMLoad = new Series("RAM %");
                    seriesRAMLoad.ChartType = SeriesChartType.Column;
                    seriesRAMLoad.ChartArea = "LoadMonitoring";

                    form.chartMonitor.Series.Add(seriesRAMLoad);
                }
                seriesRAMLoad.Points.AddXY(DateTime.Now.ToLongTimeString(), info.RAM_load);
                if (seriesRAMLoad.Points.Count > 1)
                    seriesRAMLoad.Points.RemoveAt(0);
            }));

            //form.label1.BeginInvoke((MethodInvoker)(delegate
            //{
            //    form.label1.Text = "CPU:\n   Temp: " + info.CPU_temp + "\n   Load: " + info.CPU_load + "\nRAM:\n   Load: " + info.RAM_load + "\n\n" +
            //                       "HDD:\n";
            //    int i = 1;
            //    foreach (var item in info.HDD_temp)
            //    {
            //        form.label1.Text += "   " + i + " Temp: " + item + "\n";
            //        i++;
            //    }
            //}));
        }

        void Handler_Screenshot(Packet packet)
        {
            Bitmap bitmap = (Bitmap)packet.Data;
            form.pictureBoxScreenMain.BeginInvoke((MethodInvoker)(delegate
            {
                form.pictureBoxScreenMain.Image = bitmap;
            }));
        }

        private void Handler_Application_Info(Packet packet)
        {
            List<ApplicationInfo> apps = (List<ApplicationInfo>)packet.Data;
            client.ApplicationInfo = apps;

            List<ListViewItem> listToAdd = new List<ListViewItem>();

            foreach (var item in apps)
            {
                ListViewItem itemToAdd = new ListViewItem((item.Name != null) ? item.Name.ToString() : "");

                itemToAdd.SubItems.Add(new ListViewItem.ListViewSubItem(itemToAdd, (item.InstallDate != null) ? item.InstallDate.ToString() : ""));
                itemToAdd.SubItems.Add(new ListViewItem.ListViewSubItem(itemToAdd, (item.Vendor != null) ? item.Vendor.ToString() : ""));
                itemToAdd.SubItems.Add(new ListViewItem.ListViewSubItem(itemToAdd, (item.Version != null) ? item.Version.ToString() : ""));

                listToAdd.Add(itemToAdd);
            }

            form.listViewApps.BeginInvoke((MethodInvoker)(delegate
            {
                form.listViewApps.Items.Clear();
                form.listViewApps.Items.AddRange(listToAdd.ToArray());
            }));

            if (form.StatusDBServer)
            {
                ClientDB clientDB = client;
                form.clientDBCollection.UpdateOne(clientDB.GetFilter(), clientDB.GetUpdate(), new UpdateOptions() { IsUpsert = true });
            }

        }

        void Handler_Hardware_Info(Packet packet)
        {
            Dictionary<string, string> keyValuePairs = (Dictionary<string, string>)packet.Data;
            client.HardwareInfo = keyValuePairs;

            var resolution = keyValuePairs["Разрешение экрана"].Split('x');
            client.ScreenWidth = int.Parse(resolution[0]);
            client.ScreenHeight = int.Parse(resolution[1]);

            if (form.StatusDBServer)
            {
                ClientDB clientDB = client;
                form.clientDBCollection.UpdateOne(clientDB.GetFilter(), clientDB.GetUpdate(), new UpdateOptions() { IsUpsert = true });
            }
        }

        void Handler_Process_Info(Packet packet)
        {
            List<Dictionary<string, object>> keyValuePairs = (List<Dictionary<string, object>>)packet.Data;

            List<ListViewItem> listToAdd = new List<ListViewItem>();

            foreach (var item in keyValuePairs)
            {
                ListViewItem itemToAdd = new ListViewItem((item["Name"] != null) ? item["Name"].ToString() : "");

                itemToAdd.SubItems.Add(new ListViewItem.ListViewSubItem(itemToAdd, (item["Description"] != null) ? item["Description"].ToString() : ""));
                itemToAdd.SubItems.Add(new ListViewItem.ListViewSubItem(itemToAdd, (item["CPUUsage"] != null) ? item["CPUUsage"].ToString() : ""));
                itemToAdd.SubItems.Add(new ListViewItem.ListViewSubItem(itemToAdd, (item["RAMUsage"] != null) ? item["RAMUsage"].ToString() : ""));

                listToAdd.Add(itemToAdd);
            }

            form.listViewProcess.BeginInvoke((MethodInvoker)(delegate
            {
                form.listViewProcess.Items.Clear();
                form.listViewProcess.Items.AddRange(listToAdd.ToArray());
            }));
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            while (client == null) { }
            if (e.IsBinary)
            {
                Packet packet = Packet.Deserialize(e.RawData);
                if (packet.IsError)
                {
                    MessageBox.Show("Ошибка " + packet.Header + ": " + (string)packet.Data);
                }
                else
                    switch (packet.Header)
                    {
                        case "MonitorInfo":
                            Handler_Monitor_Info(packet);
                            break;
                        case "Screenshot":
                            Handler_Screenshot(packet);
                            break;
                        case "HardwareInfo":
                            Handler_Hardware_Info(packet);
                            break;
                        case "ProcessInfo":
                            Handler_Process_Info(packet);
                            break;
                        case "ApplicationInfo":
                            Handler_Application_Info(packet);
                            break;
                        case "Files/Update":
                        case "Files/ElementsFromPath":
                            {
                                Dictionary<string, bool> elements = (Dictionary<string, bool>)packet.Data;

                                string path = packet.Path;

                                form.textBoxPath.BeginInvoke((MethodInvoker)(delegate
                                {
                                    form.textBoxPath.Text = path;
                                }));

                                form.listBoxElements.BeginInvoke((MethodInvoker)(delegate
                                {
                                    form.listBoxElements.Items.Clear();
                                    if (path.Length > 3)
                                        form.listBoxElements.Items.Add(new ExplorerTreeElement()
                                        {
                                            IsFolder = false,
                                            IsRoot = true,
                                            Name = "\\..",
                                            Path = path
                                        });
                                    foreach (var item in elements)
                                        form.listBoxElements.Items.Add(new ExplorerTreeElement()
                                        {
                                            IsFolder = item.Value,
                                            Name = item.Key,
                                            Path = path
                                        });
                                }));

                                form.panelFiles.BeginInvoke((MethodInvoker)(delegate
                                {
                                    form.panelFiles.Enabled = true;
                                }));
                            }
                            break;
                        case "Files/RunFileOnPathResult":
                            {
                                MessageBox.Show("Файл успешно запущен");
                            }
                            break;
                        case "Files/UploadFileResult":
                            {
                                MessageBox.Show("Файл успешно загружен");
                            }
                            break;
                        case "Files/GetFileResult":
                            {
                                try
                                {
                                    File.WriteAllBytes(packet.Path, (byte[])packet.Data);
                                    MessageBox.Show("Файл успешно сохранен по пути: " + packet.Path);
                                }
                                catch (Exception exp)
                                {
                                    MessageBox.Show(exp.Message);
                                }

                            }
                            break;
                        case "Notify/CriticalTemp":
                            //smtpClient.Send(new MailMessage("netmonitor.client@gmail.com", EmailTo, "Warning", (string)packet.Data));
                            break;
                        default:
                            Console.WriteLine(ClientIP + " Error Packet: " + packet.Header);
                            break;
                    }
                return;
            }
        }

        protected override void OnOpen()
        {
            ClientIP = this.Context.UserEndPoint.Address.ToString();
            NewConnection(this);
            //Console.WriteLine("New connection: " + ClientIP);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            CloseConnection();
            //Console.WriteLine("Connection close: " + e.Reason);
        }

        public void Send(Packet packet)
        {
            base.SendAsync(packet, (bool complete) => { });
        }
    }
}
