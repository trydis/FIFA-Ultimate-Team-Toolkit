using UltimateTeam.Toolkit.Models.Generic;
using UltimateTeam.Toolkit.Models.Player;

namespace UltimateTeam.Toolkit.Models
{
    public class PurchasedItemsResponse
    {
        public List<DuplicateItem> DuplicateItemIdList { get; set; }

        public List<ItemData> ItemData { get; set; }
    }
}
