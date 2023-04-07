namespace ReutersCore
{
    using ProtoBuf;
    using System.Text.Json;

    [ProtoContract]
    public class MarketDataItem
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(1)]
        public string Symbol { get; set; }
        
        [ProtoMember(2)]
        public string ServiceName { get; set; }

        [ProtoMember(3)]
        public double LastPrice { get; set; }

        [ProtoMember(4)]
        public double ClosePrice { get; set; }

        [ProtoMember(5)]
        public double BestAsk { get; set; }
        [ProtoMember(6)]
        public double BestBid { get; set; }

        public static MarketDataItem NewItem(int id)
        {
            return new MarketDataItem()
            {
                Id = id,
                Symbol = "6758.T",
                ServiceName = "IDN_SELECTFEED",
                BestAsk = 0,
                BestBid = 0
            };
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}