﻿using ReutersCore;

namespace ReutersClient
{
    public enum SerializationType
    {
        Json,
        Protobuf
    }
    public class ReutersConnectionSettings
    {
        public SerializationType SerializationType { get; set; } = SerializationType.Json;

        public string SubConnectionString { get; set; }
        public string SubTopic { get; set; }
        public string PubConnectionString { get; set; }
        public string PubTopic { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public Action<MarketDataItem> Callback { get; private set; }
        public void OnUpdate(Action<MarketDataItem> callback)
        {
            Callback = callback;
        }
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