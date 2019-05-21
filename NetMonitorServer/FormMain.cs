using MongoDB.Driver;
using NetMonitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        MongoClient mongoClient = null;
        IMongoDatabase database = null;
        public IMongoCollection<Client> clientDBCollection = null;

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
            foreach (var item in listViewClients.Items)
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
            string connectionString = "mongodb://localhost:27017";
            mongoClient = new MongoClient(connectionString);
            database = mongoClient.GetDatabase("netmonitordb");
            clientDBCollection = database.GetCollection<Client>("clients");

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
            listViewClients.SmallImageList = imageList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ServerStart();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewClients.SelectedItems.Count != 1)
            {
                SelectedClient = null;
                tabControlMain.Enabled = false;
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
                    SelectedClient.Send("Monitor_Info");
                    SelectedClient.Send("Screenshot");
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

        private void pictureBoxScreenMain_Click(object sender, EventArgs e)
        {
            if (SelectedClient != null)
            {
                if (SelectedClient.Available)
                {
                    var packet = new Packet() {
                        Header = "MouseEvent/Click",
                        Data = new MouseEvent() {
                            LeftMouse = true,
                            X = 300,
                            Y = 300
                        }
                    };
                    SelectedClient.SendPacket(packet);
                }
            }
        }
    }
}
