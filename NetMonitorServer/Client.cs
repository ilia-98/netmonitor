//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NetMonitorServer
{
    public class Client : ListViewItem
    {
        private bool available = false;

        public Dictionary<string, string> HardwareInfo = null;

        public NetMonitorClient socket = null;
        public string IP;
        public string MAC;
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
