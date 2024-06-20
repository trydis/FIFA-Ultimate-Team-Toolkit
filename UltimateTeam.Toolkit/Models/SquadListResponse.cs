using Newtonsoft.Json;

namespace UltimateTeam.Toolkit.Models
{
    public class SquadListResponse
    {
        public ushort ActiveSquadId { get; set; }

        [JsonProperty("squad")]
        public List<SquadDetailsResponse>? Squads { get; set; }
    }
}
