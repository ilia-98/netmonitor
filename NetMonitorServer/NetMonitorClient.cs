using MongoDB.Bson;
using MongoDB.Driver;
using NetMonitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace NetMonitorServer
{

    public class NetMonitorClient : WebSocketBehavior
    {
        string ClientIP { get; set; }
        string ClientMAC { get { return client.MAC; } }

        FormMain form;
        Client client;

        public NetMonitorClient(FormMain value)
        {
            form = value;
        }

        void NewConnection(NetMonitorClient netMonitorClient)
        {
            form.listViewClients.BeginInvoke((MethodInvoker)(delegate
            {
                bool addtolist = true;

                client = new Client(netMonitorClient);

                string MAC_addr = client.MAC;

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
                    client.Available = true;
                    form.listViewClients.Items.Add(client);
                }

                var filter = new BsonDocument("MAC", ClientMAC);
                UpdateDefinition<Client> ToUpdate = client.ToBsonDocument();
                //form.clientDBCollection.UpdateOne(filter, ToUpdate, new UpdateOptions() { IsUpsert = true });
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
            form.label1.BeginInvoke((MethodInvoker)(delegate
            {
                form.label1.Text = "CPU:\n   Temp: " + info.CPU_temp + "\n   Load: " + info.CPU_load + "\nRAM:\n   Load: " + info.RAM_load + "\n\n" +
                                   "HDD:\n";
                int i = 1;
                foreach (var item in info.HDD_temp)
                {
                    form.label1.Text += "   " + i + " Temp: " + item + "\n";
                    i++;
                }
            }));
        }

        void Handler_Screenshot(Packet packet)
        {
            Bitmap bitmap = (Bitmap)packet.Data;
            form.pictureBox1.BeginInvoke((MethodInvoker)(delegate
            {
                form.pictureBox1.Image = bitmap;
            }));
        }

        void Handler_Hardware_Info(Packet packet)
        {
            Dictionary<string, string> keyValuePairs = (Dictionary<string, string>)packet.Data;
            client.HardwareInfo = keyValuePairs;

            form.listViewClients.BeginInvoke((MethodInvoker)(delegate
            {
                var filter = new BsonDocument("MAC", ClientMAC);
                UpdateDefinition<Client> ToUpdate = client.ToBsonDocument();
                //form.clientDBCollection.UpdateOne(filter, ToUpdate, new UpdateOptions() { IsUpsert = true });
            }));
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            while (client == null){ }
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
                    case "Monitor_Info":
                        Handler_Monitor_Info(packet);
                        break;
                    case "Screenshot":
                        Handler_Screenshot(packet);
                        break;
                    case "Hardware_Info":
                        Handler_Hardware_Info(packet);
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
            base.Send(packet);
        }
    }
}
