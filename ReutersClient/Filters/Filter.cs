using ReutersCore;

namespace ReutersClient.Filters
{
    public interface IFilter
    {
        IFilter Next { get; set; }

        Task<MarketDataItem> Process(MarketDataItem data);
       
    }
}
