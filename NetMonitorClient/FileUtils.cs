using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetMonitor;
using WebSocketSharp;

namespace NetMonitorClient
{
    public static class FileUtils
    {
        public static void ОтправитьFilesUpdate(WebSocket socket)
        {
            var path = Path.GetDirectoryName(Application.ExecutablePath);
            var packet = new Packet()
            {
                Path = path,
                Header = "Files/Update"
            };

            packet.Data = GetElementsFromPath(path);

            socket.Send(packet);
        }

        public static Dictionary<string, bool> GetElementsFromPath(string path)
        {
            var foldes = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);

            Dictionary<string, bool> elements = new Dictionary<string, bool>();

            foreach (var item in foldes)
                elements.Add(item, true);
            foreach (var item in files)
                elements.Add(item, false);

            return elements;
        }

        public static void RunFileOnPath(WebSocket socket, Packet pac)
        {
            var path = pac.Path;

            var args = (string)pac.Data;

            var packet = new Packet()
            {
                Header = "Files/RunFileOnPathResult",
                Path = path,
            };

            try
            {
                Process p = new Process();
                p.StartInfo.FileName = path;
                if (args != "")
                    p.StartInfo.Arguments = args;
                p.Start();
            }
            catch (Exception e)
            {
                packet.IsError = true;
                packet.Data = e.Message;
            }

            socket.Send(packet);
        }

        public static void ОтправитьElementsFromPath(WebSocket socket,Packet pac)
        {
            var path = pac.Path;

            var packet = new Packet()
            {
                Header = "Files/ElementsFromPath",
                Path = path,
            };

            if (!Directory.Exists(path))
            {
                packet.IsError = true;
                packet.Data = "Путь не найден";
            }
            else
            {
                packet.Data = GetElementsFromPath(path);
            }

            socket.Send(packet);
        }

        public static void UploadFile(WebSocket socket,Packet pac)
        {
            var packet = new Packet()
            {
                Header = "Files/UploadFileResult",
                Path = pac.Path,
            };

            try
            {
                using (FileStream fstream = new FileStream(pac.Path, FileMode.OpenOrCreate, FileAccess.Write,FileShare.Write))
                {
                    fstream.Write((byte[])pac.Data, 0, ((byte[])pac.Data).Length);
                }
                //File.WriteAllBytes(pac.Path, (byte[])pac.Data);
            }
            catch (Exception e)
            {
                packet.IsError = true;
                packet.Data = e.Message;
            }

            socket.Send(packet);
        }

        public static void GetFile(WebSocket socket,Packet pac)
        {
            var packet = new Packet()
            {
                Header = "Files/GetFileResult",
                Path = (string)pac.Data,
            };

            try
            {
                var bytes = File.ReadAllBytes(pac.Path);
                packet.Data = bytes;
            }
            catch (Exception e)
            {
                packet.IsError = true;
                packet.Data = e.Message;
            }

            socket.Send(packet);
        }

        public  static void DeleteFile(WebSocket socket,Packet pac)
        {
            var packet = new Packet()
            {
                Header = "Files/DeleteFileResult",
                Path = pac.Path,
            };

            try
            {
                File.Delete(pac.Path);
            }
            catch (Exception e)
            {
                packet.IsError = true;
                packet.Data = e.Message;
            }

            socket.Send(packet);
        }
    }
}
