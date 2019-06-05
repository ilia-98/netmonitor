using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace NetMonitor
{
    [Serializable]
    public struct ApplicationInfo
    {
        public string Name;
        public string InstallDate;
        public string Vendor;
        public string Version;

        public override string ToString()
        {
            return Name;
        }
    }

    [Serializable]
    public struct MonitorInfo
    {
        public float[] HDD_temp;
        public float CPU_temp;
        public float CPU_load;
        public float RAM_load;
    }

    [Serializable]
    public struct BalloonTip
    {
        public int timeout;
        public string tipTitle;
        public string tipText;
        public ToolTipIcon tipIcon;
    }

    [Serializable]
    public struct MouseEvent
    {
        public uint X;
        public uint Y;
        public bool LeftMouse;
    }

    [Serializable]
    public class Packet
    {
        public string Header { get; set; }

        public string Path { get; set; } = null;
        public object Data { get; set; } = null;

        public bool IsError { get; set; } = false;

        public DateTime Date { get; set; } = DateTime.Now;

        public static byte[] Serialize(Packet anySerializableObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, anySerializableObject);
                return memoryStream.ToArray();
            }
        }

        public static Packet Deserialize(byte[] message)
        {
            using (var memoryStream = new MemoryStream(message))
            {
                return (Packet)((new BinaryFormatter()).Deserialize(memoryStream));
            }
        }

        public static implicit operator Packet(byte[] array)
        {
            return Deserialize(array);
        }

        public static implicit operator byte[] (Packet packet)
        {
            return Serialize(packet);
        }
    }
}