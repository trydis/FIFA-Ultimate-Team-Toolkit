using System.Text.Json.Serialization;

namespace UltimateTeam.Toolkit.Models.Auth
{
    public class UserAccountInfo
    {
        [JsonPropertyName("personas")]
        public List<Persona> Personas { get; set; }
    }
}