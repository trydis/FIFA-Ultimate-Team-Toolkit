using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class WatchlistResponse
    {
        public List<AuctionInfo> Auctioninfo { get; set; }

        public uint Credits { get; set; }

        public List<DuplicateItem> DuplicateItemIdList { get; set; }

    }
}
