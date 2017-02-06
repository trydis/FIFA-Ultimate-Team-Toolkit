using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public interface IListResponse
    {
        List<AuctionInfo> AuctionInfo { get; set; }
        uint Credits { get; set; }
        List<DuplicateItem> DuplicateItemIdList { get; set; }
    }
}