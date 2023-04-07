using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReutersServer
{
    internal class Helper
    {
        public static byte[] ProtoSerialize<T>(T record)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, record);
                var bytes = stream.ToArray();
                return bytes;
            }
        }
    }
}
