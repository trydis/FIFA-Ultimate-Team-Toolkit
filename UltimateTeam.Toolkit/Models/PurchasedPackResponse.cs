using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class PurchasedPackResponse
    {
        public List<uint> AwardSetIds { get; set; }

        public List<DuplicateItem> DuplicateItemIdList { get; set; }

        public List<ItemData> ItemList { get; set; }

        public List<uint> ItemIdData { get; set; }

        public byte NumberItems { get; set; }

        public uint PurchasedPackId { get; set; }
    }
}
