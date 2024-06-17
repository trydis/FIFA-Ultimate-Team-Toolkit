using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Models
{
    public class LoginResponse
    {
        public string AuthCode { get; set; }

        public string POWSessionId { get; set; }

        public UserDataInfo Persona { get; set; }

        public Shards Shards { get; set; }

        public UserAccounts UserAccounts { get; set; }

        public Auth AuthData { get; set; }

        public LoginResponse(Shards shards, UserAccounts userAccounts, string authCode)
        {
            Persona = new UserDataInfo();
            Shards = shards;
            UserAccounts = userAccounts;
            AuthData = new Auth();
            AuthCode = authCode;
        }

        public LoginResponse()
        {
        }
    }
}