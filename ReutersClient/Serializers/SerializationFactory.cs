namespace ReutersClient
{
    internal class SerializationFactory
    {
        public static ISerializer Create(SerializationType serializationType)
        {
            if (serializationType == SerializationType.Json)
            {
                return new JsonSerializer();
            }
            else if (serializationType == SerializationType.Protobuf)
            {
                return new ProtoSerializer();
            }

            throw new NotImplementedException();
        }
    }
}
