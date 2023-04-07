using NetMQ;
using NetMQ.Sockets;
using ReutersCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReutersServer
{
    public class Server
    {
        private readonly ServerSettings _settings;

        public Server(ServerSettings settings)
        {
            _settings = settings;
        }

        public void Start()
        {
            Task.Run(StartPublishing);
            Task.Run(StartListening);
        }

        public static Server Create(Action<ServerSettings> settings)
        {
            ServerSettings serverSettings = new ServerSettings();
            settings(serverSettings);
            return new Server(serverSettings);
        }

        public void StartPublishing()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - StartPublishing");
            using (var server = new PublisherSocket(_settings.PubConnectionString))
            {
                string topic = _settings.PubTopic;
                for (int i = 0; i < 10000000; i++)
                {

                    var item = MarketDataItem.NewItem(i);
                    var json = JsonSerializer.Serialize(item);
                    Console.WriteLine(json);

                    if (_settings.SerializationType == SerializationType.Json)
                    {
                        server.SendFrame(json);
                    }
                    else if (_settings.SerializationType == SerializationType.Protobuf)
                    {
                        var proto = Helper.ProtoSerialize(item);
                        server.SendFrame(proto);
                    }
                   // var msg = $"{topic} {json}";
                    Thread.Sleep(1000);
                }
            }
        }
        public void StartListening()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - StartListening");
            var server = new SubscriberSocket(_settings.SubConnectionString);
            {
                server.Subscribe(_settings.SubTopic);
                while (true)
                {
                    string message = server.ReceiveFrameString();
                    Console.WriteLine("Subscribe request: {0}", message);

                }
            }
        }
    }
}
