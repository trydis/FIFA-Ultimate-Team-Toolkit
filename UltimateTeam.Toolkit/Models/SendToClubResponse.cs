using UltimateTeam.Toolkit.Models.Auction;
using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class SendToClubResponse
    {
        public List<TradePileItem> ItemData { get; set; }
        public DynamicObjectivesUpdates? DynamicObjectivesUpdates { get; set; }
    }
}
