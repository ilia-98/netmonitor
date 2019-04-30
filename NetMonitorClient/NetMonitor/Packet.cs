using System;
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
    public class Packet
    {
        public string Header { get; set; }
        public object Data { get; set; }

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
    }
}