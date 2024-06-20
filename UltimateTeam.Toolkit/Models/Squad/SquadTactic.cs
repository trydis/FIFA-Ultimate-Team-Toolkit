using Newtonsoft.Json;

namespace UltimateTeam.Toolkit.Models
{
    public class Tactic
    {
        public string? Formation { get; set; }
        public List<Generic.Attribute>? Instructions { get; set; }
        public long? LastUpdateTime { get; set; }
        public List<Generic.Attribute>? Positions { get; set; }
        public int? SquadId { get; set; }
        public List<Generic.Attribute>? Styles { get; set; }
        [JsonProperty("tactic")]
        public string? TacticType { get; set; }
    }
}