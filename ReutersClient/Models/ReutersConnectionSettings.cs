namespace ReutersClient
{
    public class ReutersConnectionSettings
    {
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
    }
}