using System.Text.Json.Serialization;

namespace UltimateTeam.Toolkit.Models.Club
{
    public class UserClub
    {
        [JsonPropertyName("year")]
        public string Year { get; set; }

        [JsonPropertyName("assetId")]
        public int AssetId { get; set; }

        [JsonPropertyName("teamId")]
        public int TeamId { get; set; }

        [JsonPropertyName("lastAccessTime")]
        public long LastAccessTime { get; set; }

        [JsonPropertyName("platform")]
        public string Platform { get; set; }

        [JsonPropertyName("clubName")]
        public string ClubName { get; set; }

        [JsonPropertyName("clubAbbr")]
        public string ClubAbbr { get; set; }

        [JsonPropertyName("established")]
        public long Established { get; set; }

        [JsonPropertyName("divisionOnline")]
        public int DivisionOnline { get; set; }

        [JsonPropertyName("badgeId")]
        public int BadgeId { get; set; }

        [JsonPropertyName("skuAccessList")]
        public Dictionary<string, long> SkuAccessList { get; set; }

        [JsonPropertyName("activeHomeKit")]
        public int ActiveHomeKit { get; set; }

        [JsonPropertyName("activeCaptain")]
        public long ActiveCaptain { get; set; }
    }
}