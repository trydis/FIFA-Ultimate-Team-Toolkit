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

        public uint CalculateBid()
        {
            if (CurrentPrice == 0)
                return StartingBid;

            if (CurrentPrice < 1000)
                return CurrentPrice + 50;

            if (CurrentPrice < 10000)
                return CurrentPrice + 100;

            if (CurrentPrice < 50000)
                return CurrentPrice + 250;

            if (CurrentPrice < 100000)
                return CurrentPrice + 500;

            return CurrentPrice + 1000;
        }

        public long CalculateBaseId()
        {
            var baseId = ItemData.ResourceId;
            var version = 0;

            while (baseId > 16777216)
            {
                version++;
                switch (version)
                {
                    case 1:
                        baseId -= 1342177280;
                        break;
                    case 2:
                        baseId -= 50331648;
                        break;
                    default:
                        baseId -= 16777216;
                        break;
                }
            }

            return baseId;
        }
    }
}