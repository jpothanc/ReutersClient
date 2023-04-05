using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReutersServer
{
    public class ServerSettings
    {
        public string PubConnectionString { get; set; }
        public string PubTopic { get; set; }
        public string SubConnectionString { get; set; }
        public string SubTopic { get; set; }
    }
}
