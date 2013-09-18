using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class AuctionResponse
    {
        public List<AuctionInfo> AuctionInfo { get; set; }

        public BidTokens BidTokens { get; set; }

        public uint Credits { get; set; }

        public List<Currency> Currencies { get; set; }
    }
}