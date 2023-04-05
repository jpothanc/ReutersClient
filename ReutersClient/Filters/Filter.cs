using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReutersClient.Filters
{
    public interface IFilter
    {
        IFilter Next { get; set; }

        Task<MarketDataItem> Process(MarketDataItem data);
       
    }
}
