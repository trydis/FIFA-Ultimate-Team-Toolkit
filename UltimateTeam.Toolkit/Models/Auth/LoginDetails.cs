using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Models.Auth
{
    public class LoginDetails
    {
        public string Username { get; private set; }

        public string Password { get; private set; }

        public Platform Platform { get; set; }

        public AppVersion AppVersion { get; set; }

        public LoginDetails(string username, string password, Platform platform, AppVersion appVersion)
        {
            username.ThrowIfInvalidArgument();
            password.ThrowIfInvalidArgument();
            Username = username;
            Password = password;
            Platform = platform;
            AppVersion = appVersion;
        }

        public LoginDetails()
        {

        }
    }
}