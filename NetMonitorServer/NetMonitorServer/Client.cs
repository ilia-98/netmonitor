//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp.Server;

namespace NetMonitorServer
{
    public class Client : ListViewItem
    {
        private bool available = false;

        public Dictionary<string, string> HardwareInfo = null;

        public NetMonitorClient socket;
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
