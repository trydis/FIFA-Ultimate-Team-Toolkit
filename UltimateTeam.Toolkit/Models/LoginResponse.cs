using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Models
{
    public class LoginResponse
    {
        public AuthCode AuthCode { get; set; }

        public string POWSessionId { get; set; }

        public UserDataInfo Persona { get; set; }

        public Shards Shards { get; set; }

        public UserAccounts UserAccounts { get; set; }

        public Auth AuthData { get; set; }

        public PhishingToken PhishingToken { get; set; }

        public LoginResponse()
        {
            Persona = new UserDataInfo();
            Shards = new Shards();
            UserAccounts = new UserAccounts();
            AuthData = new Auth();
            PhishingToken = new PhishingToken();
            AuthCode = new AuthCode();
        }
    }
}