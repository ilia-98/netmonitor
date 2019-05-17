using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NetMonitorServer
{
    public class Client : ListViewItem
    {
        [BsonIgnore]
        private bool available = false;

        public Dictionary<string, string> HardwareInfo = null;

        [BsonIgnore]
        public NetMonitorClient socket = null;

        public string IP { get; set; }
        public string MAC { get; set; }

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

        public Client(string IP, string MAC)
        {
            this.IP = IP;
            this.MAC = MAC;
        }

        public Client(NetMonitorClient socket)
        {
            this.socket = socket;
            //this.socket.Poll(1000, SelectMode.SelectRead);
            this.IP = socket.Context.UserEndPoint.Address.ToString();
            this.MAC = Util.GetMacAddress(IP).ToUpper();
            base.Text = this.MAC;
        }

        public void Send(string msg)
        {
            socket.Send(msg);
        }

        public override string ToString()
        {
            return MAC;
        }
    }
}
