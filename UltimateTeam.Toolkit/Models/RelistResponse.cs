using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class RelistResponse
    {
        public List<ListAuctionResponse> TradeIdList { get; set; }
        public DynamicObjectivesUpdates? DynamicObjectivesUpdates { get; set; }
    }
}