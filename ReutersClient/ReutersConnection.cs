using NetMQ;
using NetMQ.Sockets;
using System.Text.Json;

namespace ReutersClient
{

    internal class ReutersConnection : IReutersConnection, IDisposable
    {
        private readonly ReutersConnectionSettings _options;
        private Action<MarketDataItem> ?_callback;
        private CancellationTokenSource _cancellationTokenSource;
        private SubscriberSocket? _subClient;
        private PublisherSocket? _pubClient;

        public ReutersConnection(ReutersConnectionSettings options)
        {
            _options = options;
            _callback = options.Callback;
            _cancellationTokenSource = options.CancellationTokenSource;
            _subClient = new SubscriberSocket(options.SubConnectionString);
            _pubClient = new PublisherSocket(options.PubConnectionString);
            _subClient.Subscribe(options.SubTopic); 
        }
        public void Connect()
        {
            Task.Run(() => ServerListen());
        }

        private async Task ServerListen()
        {
            try
            {
                while (true)
                {
                    string? message = _subClient?.ReceiveFrameString();
                    message = message.Split('?')[1].Trim();
                    var mdItem =  JsonSerializer.Deserialize<Dictionary<string, object>>(message);
                    if (_cancellationTokenSource != null && _cancellationTokenSource.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancel Requested");
                        break;
                    }
                    OnPriceUpdate(new MarketDataItem() { Item = mdItem});
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
            if (!_subClient.IsDisposed)
            {
                _subClient?.Disconnect(_options.SubConnectionString);
                _subClient.Dispose();
            }
        }

        public void OnUpdate(Action<MarketDataItem> callback)
        {
            _callback = callback;
        }
    }
}