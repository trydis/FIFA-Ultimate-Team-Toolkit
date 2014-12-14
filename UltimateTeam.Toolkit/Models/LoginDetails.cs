using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Models
{
    public class LoginDetails
    {
        public string Username { get; private set; }

        public string Password { get; private set; }

        public string SecretAnswer { get; private set; }

        public string SecretCode { get; private set; }

        public Platform Platform { get; set; }

        public LoginDetails(string username, string password, string secretAnswer, string secretCode, Platform platform)
        {
            username.ThrowIfInvalidArgument();
            password.ThrowIfInvalidArgument();
            secretAnswer.ThrowIfInvalidArgument();

            Username = username;
            Password = password;
            SecretAnswer = secretAnswer;
            Platform = platform;
            SecretCode = secretCode;
        }
    }
}