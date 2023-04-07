using ReutersCore;

namespace ReutersClient.Filters
{
    internal class Converter : IFilter
    {
        public IFilter Next { get; set; }
        public async Task<MarketDataItem> Process(MarketDataItem data)
        {
            data.BestBid = 1;
            if (Next != null)
            {
                await Next.Process(data);
            }
            return data;
        }
    }
}
