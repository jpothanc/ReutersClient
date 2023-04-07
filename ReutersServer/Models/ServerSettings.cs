using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReutersServer
{
    public enum SerializationType
    {
        Json,
        Protobuf
    }

    public class ServerSettings
    {
        public SerializationType SerializationType { get; set; } = SerializationType.Json;
        public string PubConnectionString { get; set; }
        public string PubTopic { get; set; }
        public string SubConnectionString { get; set; }
        public string SubTopic { get; set; }
       
        public void WithProtobuf()
        {
            SerializationType = SerializationType.Protobuf;
        }
        public void WithJson()
        {
            SerializationType = SerializationType.Json;
        }
    }
}
