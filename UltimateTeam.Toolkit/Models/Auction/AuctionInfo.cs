using UltimateTeam.Toolkit.Models.Player;

namespace UltimateTeam.Toolkit.Models.Auction
{
    public class AuctionInfo
    {
        public const uint MinBid = 150;

        public long TradeId { get; set; }
        public bool? TradeOwner { get; set; }
        public ItemData? ItemData { get; set; }
        public string? TradeState { get; set; }
        public uint BuyNowPrice { get; set; }
        public int CoinsProcessed { get; set; }
        public uint CurrentBid { get; set; }
        public int Offers { get; set; }
        public bool Watched { get; set; }
        public string? BidState { get; set; }
        public uint StartingBid { get; set; }
        public int ConfidenceValue { get; set; }
        public int Expires { get; set; }
        public string? SellerName { get; set; }
        public int SellerEstablished { get; set; }
        public int SellerId { get; set; }
        public string? TradeIdStr { get; set; }

        public uint CalculateBid()
        {
            return CurrentBid == 0 ? StartingBid : CalculateNextBid(CurrentBid);
        }

        public static uint CalculateNextBid(uint currentBid)
        {
            if (currentBid == 0)
                return MinBid;

            if (currentBid < 1000)
                return currentBid + 50;

            if (currentBid < 10000)
                return currentBid + 100;

            if (currentBid < 50000)
                return currentBid + 250;

            if (currentBid < 100000)
                return currentBid + 500;

            return currentBid + 1000;
        }

        public static uint CalculatePreviousBid(uint currentBid)
        {
            if (currentBid <= MinBid)
                return 0;

            if (currentBid <= 1000)
                return currentBid - 50;

            if (currentBid <= 10000)
                return currentBid - 100;

            if (currentBid <= 50000)
                return currentBid - 250;

            if (currentBid <= 100000)
                return currentBid - 500;

            return currentBid - 1000;
        }
    }
}