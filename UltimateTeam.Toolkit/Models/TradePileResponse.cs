using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class TradePileResponse
    {
        public List<AuctionInfo> Auctioninfo { get; set; }

        public List<Currency> Currencies { get; set; }
    }
}
