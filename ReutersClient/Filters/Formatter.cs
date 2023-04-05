namespace ReutersClient.Filters
{
    internal class Formatter : IFilter
    {
        public IFilter Next { get; set; }
        public async Task<MarketDataItem> Process(MarketDataItem data)
        {
            data.Item["Ask"] = 1;
            if(Next != null)
            {
                return await Next.Process(data);
            }
            return data;
        }
    }
}
