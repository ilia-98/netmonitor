using MongoDB.Driver;
using NetMonitor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using WebSocketSharp.Server;
using System.Runtime.InteropServices;
using MongoDB.Bson;
using NetMonitorServer.Addons;
using NetMonitorServer.RemoteControl;
using System.DirectoryServices;
using System.Threading;

namespace NetMonitorServer
{
    public partial class FormMain : Form
    {
        public WebSocketServer socketServer = new WebSocketServer(IPAddress.Any, 1348);

        public List<Client> AllClients = new List<Client>();
        public SmtpClient smtpClient;
        public string EmailTo;
        Client _selectedClient = null;

        MongoClient mongoClient = null;
        IMongoDatabase database = null;

        public IMongoCollection<ClientDB> clientDBCollection = null;

        public RemoteControlClient remoteControlClient = null;


        bool isServerMainLive = false;
        public bool StatusMainServer
        {
            get { return isServerMainLive; }
            set
            {
                if (value)
                {
                    Console.WriteLine("Сервер запущен. Ожидание подключений...");
                    labelServerStatus.Text = "ONLINE";
                    labelServerStatus.ForeColor = Color.Green;
                }
                else
                {
                    Console.WriteLine("Сервер выключен.");
                    labelServerStatus.Text = "OFLINE";
                    labelServerStatus.ForeColor = Color.Red;
                }

                isServerMainLive = value;
            }
        }
        bool isServerDBLive = false;
        public bool StatusDBServer
        {
            get
            {
                return isServerDBLive;
            }
            set
            {
                if (value)
                {
                    labelServerDBStatus.Text = "ONLINE";
                    labelServerDBStatus.ForeColor = Color.Green;
                }
                else
                {
                    labelServerDBStatus.Text = "OFLINE";
                    labelServerDBStatus.ForeColor = Color.Red;
                }

                isServerDBLive = value;
            }
        }

        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                if (value == null)
                {
                    labelSelectedItem.Text = "Выбран: " + "none";
                    tabControlMain.Enabled = false;
                }
                else
                {
                    labelSelectedItem.Text = "Выбран: " + value.ToString();
                    listViewHardwareInfo.Items.AddRange(value.GetTextForLabelHardwareInfo());
                    tabControlMain.Enabled = true;
                }

                _selectedClient = value;
            }
        }

        public List<Client> GetAllClietnsFromList()
        {
            List<Client> result = new List<Client>();
            foreach (var item in listViewClients.Items)
                result.Add((Client)item);
            return result;
        }

        public void GetPCsInLan()
        {
            /// <summary>
            /// Список имен локальных компьютеров
            /// </summary>
            var root = new DirectoryEntry("WinNT:");
            List<DirectoryEntry> items = new List<DirectoryEntry>();

            foreach (DirectoryEntry dom in root.Children)
            {
                foreach (DirectoryEntry entry in dom.Children)
                {
                    if (entry.Name != "Schema")
                    {
                        if (!items.Contains(entry))
                            items.Add(entry);
                    }
                }
            }



            Console.WriteLine("Компьютеры в локальной сети:");
            listViewClients.BeginInvoke((MethodInvoker)(delegate
            {
                foreach (var new_client in items)
                {
                    bool NeedAdd = true;

                    foreach (Client item in listViewClients.Items)
                    {
                        if (item.MachineName.ToUpper() == new_client.Name.ToUpper())
                        {
                            NeedAdd = false;
                            break;
                        }
                    }
                    
                    if (NeedAdd)
                    {
                        var ipadd = Dns.GetHostAddresses(new_client.Name);
                        var ip = "";

                        if (ipadd.Length > 0)
                        {
                            ip = ipadd[0].MapToIPv4().ToString();
                        }

                        Client client = new Client(ip, Util.GetMacAddress(ip), new_client.Name) {
                            HaveInstalledClient = false,
                            ImageIndex = 2
                        };

                        Console.WriteLine(client.MachineName + " |ip: " + client.IP + " |mac: " + client.MAC);
                        listViewClients.Items.Add(client);
                    }
                }
            }));

            //Util.GetAllPCInLan();
        }

        public void ServerStop()
        {
            socketServer.Stop();
            StatusMainServer = false;
            timer1.Stop();
        }

        public void ServerStart()
        {
            smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("netmonitor.client@gmail.com", "1w3r5y7UIO");
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            socketServer.AddWebSocketService("/NetMonitorSocketService/Main", () => new NetMonitorClient(this, smtpClient, EmailTo));
            socketServer.AddWebSocketService("/NetMonitorSocketService/RemoteControl", () => new RemoteControl.RemoteControlClient(this));
            socketServer.Start();

            StatusMainServer = true;

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

            comboBoxNotifyIconType.SelectedIndex = 0;

            ImageList imageList = new ImageList();
            imageList.Images.Add(Properties.Resources.Circle_Red);
            imageList.Images.Add(Properties.Resources.Circle_Green);
            imageList.Images.Add(Properties.Resources.Circle_Blue);
            listViewClients.SmallImageList = imageList;
        }

        public void InitializeDB()
        {
            string connectionString = "mongodb://localhost:27017";
            mongoClient = new MongoClient(connectionString);
            database = mongoClient.GetDatabase("netmonitordb");
            StatusDBServer = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);

            if (StatusDBServer)
            {
                clientDBCollection = database.GetCollection<ClientDB>("clients");
                var clientDBs = clientDBCollection.Find(Builders<ClientDB>.Filter.Empty).ToList();
                foreach (var item in clientDBs)
                    listViewClients.Items.Add(item.GetClient());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeDB();
            ServerStart();
            SelectedClient = null;
            //Thread getpcsThread = new Thread(new ThreadStart(GetPCsInLan)); getpcsThread.Start();
            //MessageBox.Show(AppSettings.Get("WebServer"));
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewClients.SelectedItems.Count != 1)
            {
                SelectedClient = null;
                panelFiles.Enabled = false;
                return;
            }
            tabControlMain.Enabled = true;
            Client selectClient = (Client)listViewClients.SelectedItems[0];
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
                    if (tabControlMain.SelectedTab == tabPageMain)
                        SelectedClient.Send("Screenshot");

                    if (tabControlMain.SelectedTab == tabPageMonitoring)
                        SelectedClient.Send("MonitorInfo");
                }
            }
        }

        private void ButtonFilesUpdate_Click(object sender, EventArgs e)
        {
            if (SelectedClient != null)
            {
                if (SelectedClient.Available)
                {
                    var packet = new Packet()
                    {
                        Header = "Files/GetUpdate"
                    };

                    SelectedClient.SendPacket(packet);
                }
            }
        }

        private void ButtonFilesGet_Click(object sender, EventArgs e)
        {
            if (SelectedClient != null)
            {
                if (SelectedClient.Available)
                {
                    if (listBoxElements.SelectedIndex == -1)
                        return;
                    var item = (ExplorerTreeElement)listBoxElements.SelectedItem;

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = Path.GetFileName(item.Name);
                    //saveFileDialog.DefaultExt = Path.GetExtension(item.Name);
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var packet = new Packet()
                        {
                            Header = "Files/GetFile",
                            Path = item.Name,
                            Data = saveFileDialog.FileName
                        };

                        SelectedClient.SendPacket(packet);
                    }
                }
            }
        }

        private void ButtonFilesUpload_Click(object sender, EventArgs e)
        {
            if (SelectedClient != null)
            {
                if (SelectedClient.Available)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var bytes = File.ReadAllBytes(openFileDialog.FileName);

                        if (textBoxPath.Text[textBoxPath.Text.Length - 1] != '\\')
                            textBoxPath.Text += "\\";

                        var packet = new Packet()
                        {
                            Header = "Files/UploadFile",
                            Path = textBoxPath.Text + Path.GetFileName(openFileDialog.FileName),
                            Data = bytes
                        };

                        SelectedClient.SendPacket(packet);
                    }
                }
            }
        }

        private void ButtonFilesDelete_Click(object sender, EventArgs e)
        {

            if (SelectedClient != null)
            {
                if (SelectedClient.Available)
                {
                    if (listBoxElements.SelectedIndex == -1)
                        return;
                    var item = (ExplorerTreeElement)listBoxElements.SelectedItem;


                    var packet = new Packet()
                    {
                        Header = "Files/DeleteFile",
                        Path = item.Name,
                    };

                    SelectedClient.SendPacket(packet);
                }
            }
        }

        private void ListBoxElements_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ButtonFilesGetElements_Click(object sender, EventArgs e)
        {
            if (SelectedClient != null)
            {
                if (SelectedClient.Available)
                {
                    SelectedClient.GetElementsFromPath(textBoxPath.Text);
                }
            }
        }

        private void ListBoxElements_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedClient != null)
            {
                if (SelectedClient.Available)
                {
                    if (listBoxElements.SelectedIndex == -1)
                        return;
                    var item = (ExplorerTreeElement)listBoxElements.SelectedItem;

                    if (item.IsRoot)
                    {
                        var path = Directory.GetParent(item.Path).FullName;
                        textBoxPath.Text = path;
                        SelectedClient.GetElementsFromPath(textBoxPath.Text);
                    }
                    else
                    if (item.IsFolder)
                    {
                        textBoxPath.Text = item.Name;
                        SelectedClient.GetElementsFromPath(textBoxPath.Text);
                    }
                }
            }
        }

        private void ButtonFilesRun_Click(object sender, EventArgs e)
        {
            if (SelectedClient != null)
            {
                if (SelectedClient.Available)
                {
                    if (listBoxElements.SelectedIndex == -1)
                        return;
                    var item = (ExplorerTreeElement)listBoxElements.SelectedItem;

                    if (!item.IsFolder && !item.IsRoot)
                    {
                        var packet = new Packet()
                        {
                            Header = "Files/RunFileOnPath",
                            Path = item.Name,
                            Data = textBoxArgs.Text
                        };

                        SelectedClient.SendPacket(packet);
                    }
                }
            }
        }

        private void ButtonStartControl_Click(object sender, EventArgs e)
        {
            if (SelectedClient != null)
            {
                if (SelectedClient.Available)
                {
                    if (remoteControlClient == null)
                    {
                        var packet = new Packet()
                        {
                            Header = "RemoteControl"
                        };

                        SelectedClient.SendPacket(packet);
                    }
                    else
                    {
                        MessageBox.Show("Управление уже идет");
                    }
                }
            }
        }

        private void ButtonGetProcess_Click(object sender, EventArgs e)
        {
            if (SelectedClient != null)
            {
                if (SelectedClient.Available)
                {
                    SelectedClient.Send("ProcessInfo");
                }
            }
        }

        private void ListViewProcess_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (SelectedClient != null)
            {
                if (SelectedClient.Available)
                {
                    var notify = new BalloonTip() {
                        timeout = 0,
                        tipTitle = textBoxNotifyTitle.Text,
                        tipText = textBoxNotifyText.Text,
                        tipIcon = (ToolTipIcon)comboBoxNotifyIconType.SelectedItem
                    };
                    var packet = new Packet() {
                        Header = "ShowBalloonTip",
                        Data = notify
                    };
                    SelectedClient.SendPacket(packet);
                }
            }
        }

        private void TabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab == tabPageInfo)
            {
                if (SelectedClient != null)
                    listViewHardwareInfo.Items.AddRange(SelectedClient.GetTextForLabelHardwareInfo());
            }
        }

        private void ButtonGetApps_Click(object sender, EventArgs e)
        {
            if (SelectedClient != null)
            {
                if (SelectedClient.Available)
                {
                    SelectedClient.Send("ApplicationInfo");
                }
            }
        }

        private void ОбновитьСписокИзЛокальнойСетиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread getpcsThread = new Thread(new ThreadStart(GetPCsInLan));
            getpcsThread.Start();
        }
    }
}
