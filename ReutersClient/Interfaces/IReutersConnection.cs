namespace ReutersClient
{
    internal interface IReutersConnection
    {
        void Connect();
        bool Subscribe(string symbol, ServiceName serviceName = ServiceName.IDN_SELECTFEED);
        bool UnSubscribe(string symbol, ServiceName serviceName = ServiceName.IDN_SELECTFEED);
        void Disconnect();
        void OnUpdate(Action<MarketDataItem> callback);
    }
}