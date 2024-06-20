using UltimateTeam.Toolkit.Models.Generic;
using UltimateTeam.Toolkit.Models.Player;

namespace UltimateTeam.Toolkit.Models
{
    public class PurchasedPackResponse
    {
        public List<uint> AwardSetIds { get; set; }

        public List<DuplicateItem> DuplicateItemIdList { get; set; }

        public List<ItemData> ItemData { get; set; }

        public List<ItemData> ItemList { get; set; }

        public List<long> ItemIdList { get; set; }

        public byte NumberItems { get; set; }

        public uint PurchasedPackId { get; set; }

        public string EntitlementQuantities { get; set; }

        public DynamicObjectivesUpdates? DynamicObjectivesUpdates { get; set; }
    }
}
