using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class TradePileResponse
    {
        public List<AuctionInfo> AuctionInfo { get; set; }

        public List<Currency> Currencies { get; set; }
    }
}
