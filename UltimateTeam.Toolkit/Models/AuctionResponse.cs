using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class AuctionResponse
    {
        public List<AuctionInfo> AuctionInfo { get; set; }

        public BidTokens BidTokens { get; set; }

        public uint Credits { get; set; }

        public List<Currency> Currencies { get; set; }

        public List<DuplicateItem> DuplicateItemIdList { get; set; }

        // TODO: I have no idea what type/structure this has, only seen null
        public string ErrorState { get; set; }
    }
}