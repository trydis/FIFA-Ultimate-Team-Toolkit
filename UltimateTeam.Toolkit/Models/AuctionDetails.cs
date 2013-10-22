using System;

namespace UltimateTeam.Toolkit.Models
{
    public class AuctionDetails
    {
        public long ItemDataId { get; set; }
       
        public AuctionDuration AuctionDuration { get; set; }
        
        public uint StartingBid { get; set; }
        
        public uint BuyNowPrice { get; set; }

        public AuctionDetails(long itemDataId, AuctionDuration auctionDuration = AuctionDuration.OneHour, uint startingBid = 150, uint buyNowPrice = 0)
        {
            if (itemDataId < 1) throw new ArgumentException("Invalid itemDataId", "itemDataId");
            if (startingBid < 150) throw new ArgumentException("Starting bid can't be less than 150", "startingBid");
            if (buyNowPrice != 0 && buyNowPrice < startingBid) throw new ArgumentException("Buy now price can't be lower than starting bid", "buyNowPrice");

            ItemDataId = itemDataId;
            AuctionDuration = auctionDuration;
            StartingBid = startingBid;
            BuyNowPrice = buyNowPrice;
        }
    }
}
