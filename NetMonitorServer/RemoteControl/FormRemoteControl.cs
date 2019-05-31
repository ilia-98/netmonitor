using NetMonitor;
using NetMonitorServer.RemoteControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetMonitorServer
{
    public partial class FormRemoteControl : Form
    {
        public RemoteControlClient CLIENT { set; get; }

        double Hx { get { return (double)CLIENT.ScreenWidth / (double)pictureBoxScreen.Width; } }
        double Hy { get { return (double)CLIENT.ScreenHeight / (double)pictureBoxScreen.Height; } }


        public FormRemoteControl(RemoteControlClient client)
        {
            InitializeComponent();
            CLIENT = client;
        }

        private void FormRemoteControl_Load(object sender, EventArgs e)
        {

        }

        private void PictureBoxScreen_MouseClick(object sender, MouseEventArgs e)
        {
            double _X = Math.Ceiling(Hx * e.Location.X);
            double _Y = Math.Ceiling(Hy * e.Location.Y);

            var packet = new Packet()
            {
                Header = "MouseEvent/Click",
                Data = new MouseEvent()
                {
                    LeftMouse = true,
                    X = (uint)_X,
                    Y = (uint)_Y
                }
            };
            CLIENT.SendPacket(packet);
        }

        private void PictureBoxScreen_MouseMove(object sender, MouseEventArgs e)
        {
            double _X = Math.Ceiling(Hx * e.Location.X);
            double _Y = Math.Ceiling(Hy * e.Location.Y);

            var packet = new Packet()
            {
                Header = "MouseEvent/Move",
                Data = new MouseEvent()
                {
                    X = (uint)_X,
                    Y = (uint)_Y
                }
            };
            CLIENT.SendPacket(packet);
        }

        private void FormRemoteControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            CLIENT.Dispose();
        }

        private void PictureBoxScreen_Click(object sender, EventArgs e)
        {

        }
    }
}
