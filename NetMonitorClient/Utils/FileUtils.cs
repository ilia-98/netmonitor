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
        public static void SendFilesUpdate(WebSocket socket)
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

        public static void RunFileOnPath(WebSocket socket, Packet request)
        {
            var path = request.Path;

            var args = (string)request.Data;

            var response = new Packet()
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
                response.IsError = true;
                response.Data = e.Message;
            }

            socket.Send(response);
        }

        public static void SendElementsFromPath(WebSocket socket, Packet request)
        {
            var path = request.Path;

            var response = new Packet()
            {
                Header = "Files/ElementsFromPath",
                Path = path,
            };

            if (!Directory.Exists(path))
            {
                response.IsError = true;
                response.Data = "Путь не найден";
            }
            else
            {
                response.Data = GetElementsFromPath(path);
            }

            socket.Send(response);
        }

        public static void UploadFile(WebSocket socket, Packet request)
        {
            var response = new Packet()
            {
                Header = "Files/UploadFileResult",
                Path = request.Path,
            };

            try
            {
                using (FileStream fstream = new FileStream(request.Path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                {
                    fstream.Write((byte[])request.Data, 0, ((byte[])request.Data).Length);
                }
                //File.WriteAllBytes(pac.Path, (byte[])pac.Data);
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.Data = e.Message;
            }

            socket.Send(response);
        }

        public static void GetFile(WebSocket socket, Packet request)
        {
            var response = new Packet()
            {
                Header = "Files/GetFileResult",
                Path = (string)request.Data,
            };

            try
            {
                var bytes = File.ReadAllBytes(request.Path);
                response.Data = bytes;
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.Data = e.Message;
            }

            socket.Send(response);
        }

        public static void DeleteFile(WebSocket socket, Packet request)
        {
            var response = new Packet()
            {
                Header = "Files/DeleteFileResult",
                Path = request.Path,
            };

            try
            {
                File.Delete(request.Path);
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.Data = e.Message;
            }

            socket.Send(response);
        }
    }
}
