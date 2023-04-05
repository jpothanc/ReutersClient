using ReutersClient.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReutersClient
{

    public class ReutersClient : IReutersClient
    {
        private readonly Action<MarketDataItem> ?_callback;
        private IReutersConnection _reutersConnection;
        private BlockingCache<MarketDataItem> _cache;
        private IFilter _filter;

        public ReutersClient(ReutersConnectionSettings options)
        {
            _cache = new BlockingCache<MarketDataItem>(1000, Process);
            _reutersConnection = new ReutersConnection(options);
            _callback = options.Callback;
            _filter = FilterChainFactory.Create("");
            Connect();
        }
        public void Connect()
        {
            _reutersConnection.Connect();
            _reutersConnection.OnUpdate(OnUpdate);
        }

        public void Disconnect()
        {
            _reutersConnection.Disconnect();
        }

        public void OnUpdate(MarketDataItem data)
        {
            _cache.Add(data);

        }

        public async Task Process(MarketDataItem data)
        {
            data = await _filter.Process(data);
            _callback?.Invoke(data);
        }

        public bool Subscribe(string symbol, ServiceName serviceName = ServiceName.IDN_SELECTFEED)
        {
            return _reutersConnection.Subscribe(symbol, serviceName);
        }

        public bool UnSubscribe(string symbol, ServiceName serviceName = ServiceName.IDN_SELECTFEED)
        {
            return _reutersConnection.UnSubscribe(symbol, serviceName);
        }
    }
}
