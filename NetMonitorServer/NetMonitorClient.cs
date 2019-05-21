using MongoDB.Bson;
using MongoDB.Driver;
using NetMonitor;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            form.listView1.BeginInvoke((MethodInvoker)(delegate
            {
                bool addtolist = true;

                client = new Client(netMonitorClient);

                string MAC_addr = client.MAC;

                foreach (Client item in form.listView1.Items)
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
                    form.listView1.Items.Add(client);
                }

                var filter = new BsonDocument("MAC", ClientMAC);
                UpdateDefinition<Client> ToUpdate = client.ToBsonDocument();
                form.clientDBCollection.UpdateOne(filter, ToUpdate, new UpdateOptions() { IsUpsert = true });
            }));
        }

        void CloseConnection()
        {
            form.listView1.BeginInvoke((MethodInvoker)(delegate
            {
                foreach (Client item in form.listView1.Items)
                    if (item.MAC == client.MAC)
                    {
                        item.Available = false;
                    }
            }));
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            while (client == null)
            { }
            if (e.IsBinary)
            {
                Packet packet = Packet.Deserialize(e.RawData);
                switch (packet.Header)
                {
                    case "Monitor_Info":
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
                        break;
                    case "Screenshot":
                        Bitmap bitmap = (Bitmap)packet.Data;
                        form.pictureBox1.BeginInvoke((MethodInvoker)(delegate
                        {
                            form.pictureBox1.Image = bitmap;
                        }));
                        break;
                    case "Hardware_Info":
                        Dictionary<string, string> keyValuePairs = (Dictionary<string, string>)packet.Data;
                        client.HardwareInfo = keyValuePairs;

                        form.listView1.BeginInvoke((MethodInvoker)(delegate
                        {
                            var filter = new BsonDocument("MAC", ClientMAC);
                            UpdateDefinition<Client> ToUpdate = client.ToBsonDocument();
                            form.clientDBCollection.UpdateOne(filter, ToUpdate, new UpdateOptions() { IsUpsert = true });
                        }));

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

        public new void Send(string msg)
        {
            base.Send(msg);
        }
    }
}
