using UltimateTeam.Toolkit.Models.Player;

namespace UltimateTeam.Toolkit.Models.Consumables
{
    public class Consumable
    {
        public int? Count { get; set; }
        public int? DiscardValue { get; set; }
        public ItemData? Item { get; set; }
        public int? ResourceGameYear { get; set; }
        public long? ResourceId { get; set; }
        public int? UntradeableCount { get; set; }
    }
}
