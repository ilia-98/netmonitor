using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using NetMonitor;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NetMonitorServer
{
    public class Client : ListViewItem
    {
        [BsonIgnore]
        private bool available = false;

        public Dictionary<string, string> HardwareInfo = null;

        public List<ApplicationInfo> ApplicationInfo = null;

        bool _haveInstalledClient = true;

        [BsonIgnore]
        public NetMonitorClient socket = null;
        public string IP { get; set; }
        public string MAC { get; set; }

        private string machineName;

        [BsonIgnore]
        public int ScreenHeight = -1;

        [BsonIgnore]
        public int ScreenWidth = -1;

        [BsonIgnore]
        public bool Available
        {
            get
            {
                return available;
            }

            set
            {
                available = value;
                if (value)
                    base.ImageIndex = 1;
                else
                    base.ImageIndex = 0;
            }
        }

        public string MachineName {
            get
            {
                return machineName;
            }
            set
            {
                machineName = value;
                base.Text = value;
            }
        }

        public bool HaveInstalledClient
        {
            get
            {
                return _haveInstalledClient;
            }
            set
            {
                _haveInstalledClient = value;
            }
        }

        public string GetTextForLabelHardwareInfo()
        {
            string result = "";
            if(HardwareInfo != null)
                foreach (var item in HardwareInfo)
                    result += item.Key + ": " + item.Value + "\n";
            return result;
        }

        public Client(string IP, string MAC, string MachineName)
        {
            this.IP = IP;
            this.MAC = MAC;
            this.MachineName = MachineName;
            Available = false;
        }

        public Client(NetMonitorClient socket) : this(socket.Context.UserEndPoint.Address.ToString(), Util.GetMacAddress(socket.Context.UserEndPoint.Address.ToString()).ToUpper(), Util.GetMachineNameFromIPAddress(socket.Context.UserEndPoint.Address.ToString()))
        {
            this.socket = socket;
            //this.socket.Poll(1000, SelectMode.SelectRead);
        }

        public Client()
        {

        }

        public void GetElementsFromPath(string path)
        {
            var packet = new Packet()
            {
                Header = "Files/GetElementsFromPath",
                Path = path
            };

            SendPacket(packet);
        }

        public void RunFileOnPath(string path)
        {
            var packet = new Packet()
            {
                Header = "Files/RunFileOnPath",
                Path = path
            };

            SendPacket(packet);
        }

        public void Send(string msg)
        {
            var packet = new Packet();
            packet.Header = msg;
            socket.Send(packet);
        }

        public void SendPacket(Packet packet)
        {
            socket.Send(packet);
        }

        public override string ToString()
        {
            return MachineName;
        }
    }
}
