using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using WebSocketSharp.Server;

namespace NetMonitorServer
{
    public partial class FormMain : Form
    {
        public WebSocketServer socketServer = new WebSocketServer(IPAddress.Any, 1348);

        public List<Client> AllClients = new List<Client>();

        Client _selectedClient = null;

        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                if (value == null)
                {
                    labelSelectedItem.Text = "Выбран: " + "none";
                }
                else
                {
                    labelHardwareInfo.Text = "";

                    if (value.HardwareInfo != null)
                        foreach (var item in value.HardwareInfo)
                            labelHardwareInfo.Text += item.Key + ": " + item.Value + "\n";

                    labelSelectedItem.Text = "Выбран: " + value.MAC;
                }

                _selectedClient = value;
            }
        }

        public List<Client> GetAllClietnsFromList()
        {
            List<Client> result = new List<Client>();
            foreach (var item in listView1.Items)
                result.Add((Client)item);
            return result;
        }

        public void ServerStop()
        {
            socketServer.Stop();
            Console.WriteLine("Сервер Выключен.");
            labelServerStatus.Text = "OFLINE";
            labelServerStatus.ForeColor = Color.Red;
            timer1.Stop();
        }

        public void ServerStart()
        {
            socketServer.AddWebSocketService("/NetMonitorSocketService", () => new NetMonitorClient(this));
            socketServer.Start();
            Console.WriteLine("Сервер запущен. Ожидание подключений...");
            labelServerStatus.Text = "ONLINE";
            labelServerStatus.ForeColor = Color.Green;
            timer1.Start();
        }

        public void ServerRestart()
        {
            ServerStop();
            ServerStart();
        }

        public FormMain()
        {
            InitializeComponent();
            ImageList imageList = new ImageList();
            imageList.Images.Add(Properties.Resources.Circle_Red);
            imageList.Images.Add(Properties.Resources.Circle_Green);
            listView1.SmallImageList = imageList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ServerStart();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
            {
                SelectedClient = null;
                tabControl1.Enabled = false;
                return;
            }
            tabControl1.Enabled = true;
            Client selectClient = (Client)listView1.SelectedItems[0];
            SelectedClient = selectClient;
        }

        private void RestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerRestart();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ServerStop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (SelectedClient != null)
            {
                if (SelectedClient.Available)
                {
                    SelectedClient.Send("Monitor_Info");
                    SelectedClient.Send("Screenshot");
                }
            }
        }
    }
}
