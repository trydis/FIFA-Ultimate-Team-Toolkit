using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class WatchlistResponse : IListResponse
    {
        public virtual List<AuctionInfo> AuctionInfo { get; set; }

        public virtual uint Credits { get; set; }

        public virtual List<DuplicateItem> DuplicateItemIdList { get; set; }

        public ushort Total { get; set; }
    }
}
