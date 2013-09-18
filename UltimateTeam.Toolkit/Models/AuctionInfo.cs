namespace UltimateTeam.Toolkit.Models
{
    public class AuctionInfo
    {
        public string BidState { get; set; }

        public uint BuyNowPrice { get; set; }

        public uint CurrentPrice { get; set; }

        public int Expires { get; set; }

        public ItemData ItemData { get; set; }

        public uint Offers { get; set; }

        public string SellerEstablished { get; set; }

        public uint SellerId { get; set; }

        public string SellerName { get; set; }

        public uint StartingBid { get; set; }

        public long TradeId { get; set; }

        public string TradeState { get; set; }
    }
}