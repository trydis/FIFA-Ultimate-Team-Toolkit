using System.Text.Json.Serialization;
using UltimateTeam.Toolkit.Models.Club;

namespace UltimateTeam.Toolkit.Models.Auth
{
    public class Persona
    {
        [JsonPropertyName("personaId")]
        public long PersonaId { get; set; }

        [JsonPropertyName("personaName")]
        public string PersonaName { get; set; }

        [JsonPropertyName("returningUser")]
        public int ReturningUser { get; set; }

        [JsonPropertyName("onlineAccess")]
        public bool OnlineAccess { get; set; }

        [JsonPropertyName("trial")]
        public bool Trial { get; set; }

        [JsonPropertyName("userState")]
        public object UserState { get; set; } // You might want to define a proper type for this

        [JsonPropertyName("userClubList")]
        public List<UserClub> UserClubList { get; set; }

        [JsonPropertyName("trialFree")]
        public bool TrialFree { get; set; }
    }
}