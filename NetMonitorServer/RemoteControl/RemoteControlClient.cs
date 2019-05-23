using NetMonitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace NetMonitorServer.RemoteControl
{
    public class RemoteControlClient : WebSocketBehavior
    {
        public FormMain FormMain { get; set; }
        public static FormRemoteControl FormControl { get; set; } = null;

        public int ScreenHeight = -1;

        public int ScreenWidth = -1;

        Thread formThread;

        public RemoteControlClient(FormMain _formMain)
        {
            FormMain = _formMain;
        }

        protected void SetScreenResolution(Packet packet)
        {
            Dictionary<string, int> resolutionDic = (Dictionary<string, int>)packet.Data;
            ScreenWidth = resolutionDic["Width"];
            ScreenHeight = resolutionDic["Height"];

            SendPacket(new Packet() {
                Header = "RemoteControl/Start"
            });

            formThread = new Thread(() => {
                try
                {
                    FormControl = new FormRemoteControl(this);
                    FormControl.Text = Context.UserEndPoint.Address.ToString() + " | " + Util.GetMacAddress(Context.UserEndPoint.Address.ToString()).ToUpper();
                    FormControl.ShowDialog();
                }
                catch { }
            });
            formThread.Start();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Packet packet = Packet.Deserialize(e.RawData);
            if (packet.IsError)
            {
                MessageBox.Show("Ошибка " + packet.Header + ": " + (string)packet.Data);
            }
            else
                switch (packet.Header)
                {
                    case "RemoteControl/Resolution":
                        SetScreenResolution(packet);
                        break;
                    case "RemoteControl/Screen":
                        {
                            FormControl.pictureBoxScreen.Image = (Bitmap)packet.Data;
                        }
                        break;
                }
        }

        protected override void OnOpen()
        {

        }

        protected override void OnError(ErrorEventArgs e)
        {
            formThread.Abort();
        }

        public void SendPacket(Packet packet)
        {
            Send(packet);
        }
    }
}
