using UltimateTeam.Toolkit.Models.Player;

namespace UltimateTeam.Toolkit.Models
{
    public class SquadPlayer
    {
        public ushort? Index { get; set; }

        public ItemData? ItemData { get; set; }

        public ushort? KitNumber { get; set; }

        public ushort? LoyaltyBonus { get; set; }

        public ushort? Rank { get; set; }

        public string? Chemistry { get; set; }

        public PlayerAttributes? PlayerAttributes { get; set; }
    }
}