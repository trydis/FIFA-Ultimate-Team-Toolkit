using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class PurchasedItemsResponse
    {
        public List<DuplicateItem> DuplicateItemIdList { get; set; }

        public List<ItemData> ItemData { get; set; }
    }
}
