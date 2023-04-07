using NetMQ;
using NetMQ.Sockets;
using ReutersCore;

namespace ReutersClient
{

    internal class ReutersConnection : IReutersConnection, IDisposable
    {
        private readonly ReutersConnectionSettings _options;
        private Action<MarketDataItem> ?_callback;
        private CancellationTokenSource _cancellationTokenSource;
        private SubscriberSocket? _subClient;
        private PublisherSocket? _pubClient;
        private ISerializer _serializer;

        public ReutersConnection(ReutersConnectionSettings options)
        {
            _options = options;
            _callback = options.Callback;
            _cancellationTokenSource = options.CancellationTokenSource;
            _subClient = new SubscriberSocket(options.SubConnectionString);
            _pubClient = new PublisherSocket(options.PubConnectionString);
            _subClient.Subscribe(options.SubTopic);
            _serializer = SerializationFactory.Create(_options.SerializationType);
        }
        public void Connect()
        {
            Task.Run(() => DoListen());
        }

        private async Task DoListen()
        {
            try
            {
                while (true)
                {
                    var message = _subClient?.ReceiveFrameBytes();

                    if (message == null)
                        continue;
                    
                    var mdItem = _serializer.Deserialize<MarketDataItem>(message);

                    if (_cancellationTokenSource != null && _cancellationTokenSource.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancel Requested");
                        break;
                    }
                    
                    OnPriceUpdate(mdItem);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Disconnect()
        {
            Dispose();
        }

        private void OnPriceUpdate(MarketDataItem data)
        {
            _callback?.Invoke(data);
        }

        public bool Subscribe(string symbol, ServiceName serviceName = ServiceName.IDN_SELECTFEED)
        {
            _pubClient?.SendFrame($"{_options.PubTopic} {ServiceName.IDN_SELECTFEED.ToString()}:{symbol}");
            return true;
        }

        public bool UnSubscribe(string symbol, ServiceName serviceName = ServiceName.IDN_SELECTFEED)
        {
            return true;
        }
        public void Dispose()
        {
            if (_subClient != null && !_subClient.IsDisposed)
            {
                _subClient?.Disconnect(_options.SubConnectionString);
                _subClient?.Dispose();
            }
        }

        public void OnUpdate(Action<MarketDataItem> callback)
        {
            _callback = callback;
        }
    }
}