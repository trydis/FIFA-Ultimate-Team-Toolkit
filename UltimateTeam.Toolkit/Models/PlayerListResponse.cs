using UltimateTeam.Toolkit.Models.Player;

namespace UltimateTeam.Toolkit.Models
{
    public class PlayerListResponse
    {
        public List<PlayerDefinition>? LegendsPlayers { get; set; }
        public List<PlayerDefinition>? Players { get; set; }
    }
}