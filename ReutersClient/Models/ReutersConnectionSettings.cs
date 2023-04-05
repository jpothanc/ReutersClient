namespace ReutersClient
{
    public class ReutersConnectionSettings
    {
        public string ConnectionString { get; set; }
        public string Topic { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public Action<MarketDataItem> Callback { get; private set; }
        public void OnUpdate(Action<MarketDataItem> callback)
        {
            Callback = callback;
        }
    }
}