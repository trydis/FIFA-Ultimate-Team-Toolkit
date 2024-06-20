using Newtonsoft.Json;

namespace UltimateTeam.Toolkit.Models.Player
{
    public class PlayerAttributes
    {
        public List<int>? BaseTraits { get; set; }
        public int? DefId { get; set; }
        public List<int>? IconTraits { get; set; }
        [JsonProperty("ingameattribs")]
        public List<int>? InGameAttributes { get; set; }
        public List<int>? MainGoalkeepAttributes { get; set; }
        public List<int>? MainOutfieldAttributes { get; set; }
        public ushort? Rating { get; set; }
        public List<int>? rolePlus { get; set; }
        public List<int>? rolePlusPlus { get; set; }
        public long? Version { get; set; }
    }
}
