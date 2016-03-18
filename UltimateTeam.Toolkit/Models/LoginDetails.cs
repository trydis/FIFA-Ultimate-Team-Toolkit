using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Models
{
    public class LoginDetails
    {
        public string Username { get; private set; }
        
        public string Password { get; private set; }
        
        public string SecretAnswer { get; private set; }

        public Platform Platform { get; set; }

        public AppVersion AppVersion { get; set; }

        public LoginDetails(string username, string password, string secretAnswer, Platform platform, AppVersion appVersion)
        {
            username.ThrowIfInvalidArgument();
            password.ThrowIfInvalidArgument();
            secretAnswer.ThrowIfInvalidArgument();
            Username = username;
            Password = password;
            SecretAnswer = secretAnswer;
            Platform = platform;
            AppVersion = appVersion;
        }
    }
}