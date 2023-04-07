using ProtoBuf;

namespace ReutersClient
{
    internal class ProtoSerializer : ISerializer
    {
        public SerializationType SerializationType { get; } = SerializationType.Protobuf;
        public T Deserialize<T>(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                var person = Serializer.Deserialize<T>(stream);
                return person;
            }
        }

        public byte[] Serialize<T>(T record)
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
