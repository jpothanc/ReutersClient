using System.Text;

namespace ReutersClient
{
    internal class JsonSerializer : ISerializer
    {
        public SerializationType SerializationType { get; } = SerializationType.Json;
        public T Deserialize<T>(byte[] bytes)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(bytes);

        }

        public byte[] Serialize<T>(T record)
        {
            var json = System.Text.Json.JsonSerializer.Serialize<T>(record);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
