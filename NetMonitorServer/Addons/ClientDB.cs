using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMonitorServer.Addons
{
    public class ClientDB
    {
        [BsonIgnoreIfNull]
        public string IP { get; set; } = null;

        [BsonIgnoreIfNull]
        public Dictionary<string, string> HardwareInfo = null;

        public string MAC { get; set; }

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

        public static implicit operator ClientDB(Client client)
        {
            ClientDB clientDB = new ClientDB()
            {
                IP = client.IP,
                MAC = client.MAC,
                HardwareInfo = client.HardwareInfo,
            };
            return clientDB;
        }
    }
}
