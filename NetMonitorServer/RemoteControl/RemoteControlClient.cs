using NetMonitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace NetMonitorServer.RemoteControl
{
    public class RemoteControlClient : WebSocketBehavior , IDisposable
    {
        public FormMain formMain { get; set; }
        public static FormRemoteControl FormControl { get; set; } = null;

        public int ScreenHeight = -1;

        public int ScreenWidth = -1;

        Thread formThread;
        Thread screenThread;

        TcpListener tcpListenerForScreenShare = null;

        public RemoteControlClient(FormMain _formMain)
        {
            screenThread = new Thread(() => {
                tcpListenerForScreenShare = new TcpListener(1350);
                tcpListenerForScreenShare.Start();
                var client = tcpListenerForScreenShare.AcceptTcpClient();
                var stream = client.GetStream();
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Жду экран");
                        Bitmap img = null;
                        //img = (Bitmap)Image.FromStream(stream);

                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                        {
                            byte[] data = new byte[65536];
                            do
                            {
                                int bytes = stream.Read(data, 0, data.Length);
                                Console.WriteLine("Принял байт: " + bytes);
                                ms.Write(data, 0, bytes);
                            }
                            while (stream.DataAvailable);
                            img = (Bitmap)Image.FromStream(ms);
                            ms.Dispose();
                        }

                        FormControl.pictureBoxScreen.BeginInvoke((MethodInvoker)(delegate
                        {
                            FormControl.pictureBoxScreen.Image = img;
                        }));
                    }
                    catch (System.IO.IOException exp)
                    {
                        return;
                    }
                    catch (System.InvalidOperationException exp)
                    {
                        
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Ошибка: " + exp.Message + " | в screenThread");
                    }
                }
            });
            screenThread.Start();

            formMain = _formMain;
            formMain.remoteControlClient = this;
        }

        ~RemoteControlClient()
        {
            
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
            try
            {
                formThread.Abort();
            }
            catch
            { }
            screenThread.Abort();
            this.Dispose();
        }

        protected override void OnClose(CloseEventArgs e)
        {
            try
            {
                formThread.Abort();
            }
            catch
            { }
            screenThread.Abort();
            this.Dispose();
        }

        public void SendPacket(Packet packet)
        {
            Send(packet);
        }

        public void Dispose()
        {
            SendPacket(new Packet()
            {
                Header = "RemoteControl/Stop"
            });
            formMain.remoteControlClient = null;
            screenThread.Abort();
            tcpListenerForScreenShare.Stop();
        }
    }
}
