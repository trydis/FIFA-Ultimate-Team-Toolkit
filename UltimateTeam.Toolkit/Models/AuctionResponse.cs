using UltimateTeam.Toolkit.Models.Auction;
using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class AuctionResponse
    {
        public List<AuctionInfo>? AuctionInfo { get; set; }

        public BidTokens? BidTokens { get; set; }

        public uint Credits { get; set; }

        public List<Currency>? Currencies { get; set; }

        public List<DuplicateItem>? DuplicateItemIdList { get; set; }

        public DynamicObjectivesUpdates? DynamicObjectivesUpdates { get; set; }

        public string? ErrorState { get; set; }
    }
}