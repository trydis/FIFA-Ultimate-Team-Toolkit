using Newtonsoft.Json;

namespace UltimateTeam.Toolkit.Models.Player
{
    public class PlayerDefinition
    {
        public uint Id { get; set; }

        [JsonProperty("c")]
        public string? CommonName { get; set; }

        [JsonProperty("f")]
        public string? Firstname { get; set; }

        [JsonProperty("l")]
        public string? Lastname { get; set; }

        [JsonProperty("r")]
        public ushort Rating { get; set; }
    }
}