using UltimateTeam.Toolkit.Models.Auth;

namespace UltimateTeam.Toolkit.Models
{
    public class LoginResponse
    {
        public string? NucleusId { get; set; }

        public string? AccessToken { get; set; }

        public PidData? PersonId { get; set; }

        public UserAccounts? UserAccounts { get; set; }

        public Persona? DefaultPersona { get; set; }

        public Auth.AuthInfo? AuthData { get; set; }
    }
}