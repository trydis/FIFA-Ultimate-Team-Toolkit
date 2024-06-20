using Newtonsoft.Json;
using UltimateTeam.Toolkit.Models.Store;

namespace UltimateTeam.Toolkit.Models
{
    public class StoreResponse
    {
        public string? Id { get; set; }
        [JsonProperty("purchase")]
        public List<Pack>? Packs { get; set; }
        public string? TimeStamp { get; set; }
        public string? PackOddsAvailabilityState { get; set; }
        public bool? PreviewEnabled { get; set; }
        public bool? DuplicatesEnabled { get; set; }
        public StoreRecommendations? StoreRecommendations { get; set; }
    }
}