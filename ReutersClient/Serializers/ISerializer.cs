using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReutersClient
{
    internal interface ISerializer
    {
        SerializationType SerializationType { get; }
        T Deserialize<T>(byte[] bytes);
        byte[] Serialize<T>(T record);
    }
}
