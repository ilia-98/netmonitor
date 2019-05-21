using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NetMonitor
{
    [Serializable]
    public struct MonitorInfo
    {
        public float[] HDD_temp;
        public float CPU_temp;
        public float CPU_load;
        public float RAM_load;
    }

    [Serializable]
    public struct MouseEvent
    {
        public Point Point;
        public bool LeftMouse;
    }

    [Serializable]
    public class Packet
    {
        public string Header { get; set; }

        public string Path { get; set; } = null;
        public object Data { get; set; } = null;

        public bool IsError { get; set; } = false;

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