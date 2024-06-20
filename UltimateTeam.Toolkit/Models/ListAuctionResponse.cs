using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class ListAuctionResponse
    {
        public long Id { get; set; }
        public string? idStr { get; set; }
        public DynamicObjectivesUpdates? DynamicObjectivesUpdates { get; set; }
    }
}
