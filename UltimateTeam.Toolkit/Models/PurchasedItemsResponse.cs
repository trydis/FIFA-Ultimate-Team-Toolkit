using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class PurchasedItemsResponse
    {
        public List<DuplicateItem> DuplicateItemList { get; set; }

        public List<ItemData> itemdata { get; set; }
    }
}
