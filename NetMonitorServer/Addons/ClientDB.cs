using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using NetMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMonitorServer.Addons
{
    [BsonIgnoreExtraElements]
    public class ClientDB
    {
        [BsonId]
        [BsonIgnoreIfNull]
        public string MAC { get; set; } = null;

        [BsonIgnoreIfNull]
        public string IP { get; set; } = null;

        [BsonIgnoreIfNull]
        public string MachineName { get; set; } = null;

        [BsonIgnoreIfNull]
        public Dictionary<string, string> HardwareInfo = null;

        //[BsonIgnoreIfNull]
        //public List<ApplicationInfo> ApplicationInfo = null;

        public BsonDocument GetUpdate()
        {
            var _filter = this.ToBsonDocument();
            var filter = new BsonDocument { { "$set", _filter } };
            return filter.ToBsonDocument();
        }

        public BsonDocument GetFilter()
        {
            var filter = new BsonDocument("MAC", this.MAC);
            return filter.ToBsonDocument();
        }

        public Client GetClient()
        {
            Client client = new Client(IP, MAC, MachineName)
            {
                HardwareInfo = HardwareInfo,
            };
            return client;
        }

        public static implicit operator ClientDB(Client client)
        {
            ClientDB clientDB = new ClientDB()
            {
                IP = client.IP,
                MAC = client.MAC,
                MachineName = client.MachineName,
                HardwareInfo = client.HardwareInfo,
                //ApplicationInfo = client.ApplicationInfo,
            };
            return clientDB;
        }
    }
}
