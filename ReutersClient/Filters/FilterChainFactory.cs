using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReutersClient.Filters
{
    public  class FilterChainFactory
    {

        public static IFilter Create(string filters)
        {
            IFilter filter1 = new Formatter();
            IFilter filter2 = new Converter();

            filter1.Next = filter2;
            return filter1;
        }
    }
}
