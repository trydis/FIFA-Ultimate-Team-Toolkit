using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Services;

namespace UltimateTeam.Toolkit.Requests
{
    internal class LoginRequest : FutRequestBase, IFutRequest<LoginResponse>
    {
        private IHasher _hasher;
        private ITwoFactorCodeProvider _twoFactorCodeProvider;
        private AuthenticationType _authType;

        public IHasher Hasher
        {
            get { return _hasher ?? (_hasher = new Hasher()); }
            set { _hasher = value; }
        }

        public AuthenticationType AuthType
        {
            get { return _authType; }
            set { _authType = value; }
        }

        public ITwoFactorCodeProvider TwoFactorCodeProvider
        {
            get { return _twoFactorCodeProvider; }
            set { _twoFactorCodeProvider = value; }
        }

        public LoginRequest(LoginDetails loginDetails, ITwoFactorCodeProvider twoFactorCodeProvider)
        {
            if (loginDetails.Username == null || loginDetails.Password == null)
            {
                throw new FutException($"No Username or Password provided for {LoginDetails?.AppVersion}.");
            }
            LoginDetails = loginDetails;
            TwoFactorCodeProvider = twoFactorCodeProvider;
        }

        public async Task<LoginResponse> PerformRequestAsync()
        {
            try
            {
                var mainPageResponseMessage = await GetMainPageAsync().ConfigureAwait(false);
                var loginResponseMessage = await LoginAsync(mainPageResponseMessage);
                var accessToken = LoginResponse.AuthCode;
                var pidData = await GetPidDataAsync(accessToken);

                LoginResponse.Persona.NucUserId = pidData.Pid.ExternalRefValue;

                var sessionCode = await GetAuthCodeAsync(accessToken);
                LoginResponse.AuthCode = await GetAuthCodeAsync(accessToken);

                LoginResponse.Shards = await GetShardsAsync();
                LoginResponse.UserAccounts = await GetUserAccountsAsync(LoginDetails);

                var matchingPersona = MatchPersona(LoginResponse.UserAccounts);

                LoginResponse.Persona.NucPersId = matchingPersona.PersonaId;
                LoginResponse.Persona.DisplayName = matchingPersona.PersonaName;

                LoginResponse.AuthData = await AuthAsync();

                return LoginResponse;
            }
            catch (Exception e)
            {
                throw new FutException($"Unable to login to {LoginDetails.AppVersion}", e);
            }
        }

        private async Task<PidData> GetPidDataAsync(string authCode)
        {
            AddLoginHeaders();
            AddAuthorizationHeader(authCode);
            var pidDataResponseMessage = await HttpClient.GetAsync(string.Format(Resources.Pid));
            var pidData = await DeserializeAsync<PidData>(pidDataResponseMessage);

            if (pidData == null || pidData.Pid == null)
                throw new Exception($"Got no PID Data during the Loginprocess to to {LoginDetails?.AppVersion}.");
            return pidData;
        }

        private async Task<string> GetAuthCodeAsync(string accessToken)
        {
            AddLoginHeaders();
            var authCodeResponseMessage = await HttpClient.GetAsync(string.Format(Resources.AuthCode, accessToken));
            var authCode = await DeserializeAsync<AuthCode>(authCodeResponseMessage);

            if (authCode == null || authCode.Code == null)
                throw new Exception($"Got no AuthCode during the Loginprocess to {LoginDetails?.AppVersion}.");

            return authCode.Code;
        }

        protected async Task<HttpResponseMessage> SetTwoFactorTypeAsync(HttpResponseMessage mainPageResponseMessage)
        {
            var loginResponseMessage = new HttpResponseMessage();
            var contentData = string.Empty;

            if (_authType == AuthenticationType.Email)
            {
                loginResponseMessage = await HttpClient.PostAsync(mainPageResponseMessage.RequestMessage.RequestUri, new FormUrlEncodedContent(
                                                                                                                            new[]
                                                                                                                            {
                                                                                                                            new KeyValuePair<string, string>("_eventId", "submit"),
                                                                                                                            }));
            }
            else
            {
                loginResponseMessage = await HttpClient.PostAsync(mainPageResponseMessage.RequestMessage.RequestUri, new FormUrlEncodedContent(
                                                                                                                            new[]
                                                                                                                            {
                                                                                                                            new KeyValuePair<string, string>("_eventId", "submit"),
                                                                                                                            new KeyValuePair<string, string>("codeType", "APP"),
                                                                                                                            }));
            }

            return loginResponseMessage;
        }

        protected async Task<HttpResponseMessage> SetTwoFactorCodeAsync(HttpResponseMessage loginResponse)
        {
            var contentData = await loginResponse.Content.ReadAsStringAsync();
            loginResponse = await LoginForwarder(loginResponse);
            contentData = await loginResponse.Content.ReadAsStringAsync();

            _authType = AuthenticationType.Unknown;
            var sended = await SetTwoFactorTypeAsync(loginResponse);
            loginResponse = await LoginForwarder(sended);

            if (contentData.Contains("send you a code to:"))
            {
                _authType = AuthenticationType.Email;
            }
            else if (contentData.Contains("App Authenticator"))
            {
                _authType = AuthenticationType.App;
            }

            var twoFactorCode = await _twoFactorCodeProvider.GetTwoFactorCodeAsync(_authType);

            if (twoFactorCode.Length != 6)
            {
                throw new Exception($"Two Factor Code MUST be 6 digits long {LoginDetails?.AppVersion}.");
            }

            if (_authType ==AuthenticationType.Unknown)
            {
                throw new Exception($"Unable to determine AuthType (i.e. App Authenticator or E-Mail) for {LoginDetails?.AppVersion}.");
            }

            AddRefererHeader(loginResponse.RequestMessage.RequestUri.ToString());

            var codeResponseMessage = await HttpClient.PostAsync(loginResponse.RequestMessage.RequestUri,
                new FormUrlEncodedContent(
                    new[]
                    {
                        new KeyValuePair<string, string>("oneTimeCode", twoFactorCode),
                        new KeyValuePair<string, string>("_eventId", "submit"),
                        new KeyValuePair<string, string>("trustThisDevice", "on"),
                        new KeyValuePair<string, string>("_trustThisDevice", "on"),
                    }));

            var codeResponseMessageContent = await codeResponseMessage.Content.ReadAsStringAsync();

            if (codeResponseMessageContent.Contains("Incorrect code entered"))
            {
                throw new Exception($"Incorrect Two Factor Code entered for {LoginDetails?.AppVersion}.");
            }

            return codeResponseMessage;
        }

        protected async Task<HttpResponseMessage> LoginForwarder(HttpResponseMessage responseMessage)
        {
            HttpResponseMessage forwardResponseMessage = new HttpResponseMessage();
            var contentData = await responseMessage.Content.ReadAsStringAsync();
            if (contentData.Contains("https://signin.ea.com:443/p/web2/login?execution="))
            {
                Match executionIdMatch = Regex.Match(contentData, @"https:\/\/signin\.ea\.com:443\/p\/web2\/login\?execution=([A-Za-z0-9\-]+)");

                string executionId = executionIdMatch.Groups[1].Value;
                forwardResponseMessage = await HttpClient.GetAsync("https://signin.ea.com:443/p/web2/login?execution=" + executionId + "&_eventId=end");

                return forwardResponseMessage;
            }
            return responseMessage;
        }

        protected async Task<Shards> GetShardsAsync()
        {
            AddLoginHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusId, LoginResponse.Persona.NucUserId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.PowSessionId, LoginResponse.POWSessionId);

            var shardsResponseMessage = await HttpClient.GetAsync(string.Format(Resources.Shards, DateTime.Now.ToUnixTime()));
            var shardsResponseContent = await shardsResponseMessage.Content.ReadAsStringAsync();
            var shards = await DeserializeAsync<Shards>(shardsResponseMessage);

            if (shards?.ShardInfo == null || shards.ShardInfo.Count <= 0)
            {
                throw new Exception($"Unable to get Shards {LoginDetails?.AppVersion}.");
            }
            return shards;
        }

        protected async Task<UserAccounts> GetUserAccountsAsync(LoginDetails LoginDetails)
        {
            AddLoginHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusId, LoginResponse.Persona.NucUserId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, string.Empty);

            var accountInfoResponseMessage = await HttpClient.GetAsync(string.Format(Resources.AccountInfo, DateTime.Now.ToUnixTime()));
            var accountInfoResponseMessageContent = await accountInfoResponseMessage.Content.ReadAsStringAsync();
            var userAccounts = await DeserializeAsync<UserAccounts>(accountInfoResponseMessage);

            if (userAccounts?.UserAccountInfo?.Personas == null || userAccounts.UserAccountInfo.Personas.Count() <= 0)
            {
                throw new Exception($"Unable to get Personas {LoginDetails?.AppVersion}.");
            }
            return userAccounts;
        }

        private Persona MatchPersona(UserAccounts userAccounts)
        {
            var matchingPersona = new Persona();
            try
            {
                matchingPersona = LoginResponse.UserAccounts.UserAccountInfo.Personas.First(n => n.UserClubList.First().Platform ==     GetPlatform(LoginDetails.Platform));
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to match a valid Persona for {LoginDetails?.AppVersion}.", e);
            }
            return matchingPersona;
        }

        protected async Task<PhishingToken> ValidateAsync(LoginDetails loginDetails)
        {
            AddLoginHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusId, LoginResponse.Persona.NucUserId);

            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, LoginResponse.AuthData.Sid);
            var validateResponseMessage = await HttpClient.GetAsync(String.Format(Resources.ValidateQuestion, DateTime.Now.ToUnixTime()));
            validateResponseMessage = await HttpClient.PostAsync(String.Format(Resources.ValidateAnswer, Hasher.Hash(loginDetails.SecretAnswer)), new FormUrlEncodedContent(
                  new[]
                  {
                    new KeyValuePair<string, string>("answer", Hasher.Hash(loginDetails.SecretAnswer))
                  }));
            var phishingToken = await DeserializeAsync<PhishingToken>(validateResponseMessage);
            var validateResponseMessageContent = await validateResponseMessage.Content.ReadAsStringAsync();

            if (phishingToken.Code != "200" || phishingToken.Token == null)
            {
                throw new Exception($"Unable to get Phishing Token {LoginDetails?.AppVersion}.");
            }

            return phishingToken;

        }

        protected async Task<Auth> AuthAsync()
        {
            string httpContent;
            var authResponseMessage = new HttpResponseMessage();

            AddLoginHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, string.Empty);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.PowSessionId, string.Empty);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.Origin, @"file://");
            httpContent = $@"{{""isReadOnly"":false,""sku"":""{Resources.Sku}"",""clientVersion"":{Resources.ClientVersion},""locale"":""en-US"",""method"":""authcode"",""priorityLevel"":4,""identification"":{{""authCode"":""{LoginResponse.AuthCode}"",""redirectUrl"":""nucleus:rest""}},""nucleusPersonaId"":""{LoginResponse.Persona.NucPersId}"",""gameSku"":""{GetGameSku(LoginDetails.Platform)}""}}";
            authResponseMessage = await HttpClient.PostAsync(string.Format(String.Format(Resources.Auth, DateTime.Now.ToUnixTime()), DateTime.Now.ToUnixTime()), new StringContent(httpContent));

            var authResponse = await DeserializeAsync<Auth>(authResponseMessage);
            var authResponseContent = await authResponseMessage.Content.ReadAsStringAsync();

            if (authResponse.Sid == null)
            {
                throw new Exception($"Unable to get Session Id {LoginDetails?.AppVersion}.");
            }

            return authResponse;
        }

        protected async Task<HttpResponseMessage> LoginAsync(HttpResponseMessage mainPageResponseMessage)
        {
            var loginResponseMessage = new HttpResponseMessage();
            var contentData = string.Empty;

            loginResponseMessage = await HttpClient.PostAsync(mainPageResponseMessage.RequestMessage.RequestUri, new FormUrlEncodedContent(
                                                                                                                        new[]
                                                                                                                        {
                                                                                                                            new KeyValuePair<string, string>("email", LoginDetails.Username),
                                                                                                                            new KeyValuePair<string, string>("password", LoginDetails.Password),
                                                                                                                            new KeyValuePair<string, string>("_eventId", "submit"),
                                                                                                                            new KeyValuePair<string, string>("country", "UK"),
                                                                                                                            new KeyValuePair<string, string>("phoneNumber", ""),
                                                                                                                            new KeyValuePair<string, string>("passwordForPhone", ""),
                                                                                                                            new KeyValuePair<string, string>("_rememberMe", "on"),
                                                                                                                            new KeyValuePair<string, string>("rememberMe", "on"),
                                                                                                                            new KeyValuePair<string, string>("gCaptchaResponse", ""),
                                                                                                                            new KeyValuePair<string, string>("isPhoneNumberLogin", "false"),
                                                                                                                            new KeyValuePair<string, string>("isIncompletePhone", "")
                                                                                                                        }));
            contentData = await loginResponseMessage.Content.ReadAsStringAsync();

            if (contentData.Contains("Your credentials are incorrect or have expired") || contentData.Contains("Email address is invalid"))
            {
                throw new Exception($"Wrong credentials for {LoginDetails?.AppVersion}.");
            }

            var forwardedResponse = await LoginForwarder(loginResponseMessage);
            contentData = await forwardedResponse.Content.ReadAsStringAsync();
            loginResponseMessage = forwardedResponse;


            if (contentData.Contains("Login Verification"))
            {
                loginResponseMessage = await SetTwoFactorCodeAsync(loginResponseMessage);
                contentData = await loginResponseMessage.Content.ReadAsStringAsync();
            }

            if (loginResponseMessage.RequestMessage.RequestUri.AbsoluteUri.Contains("access_token="))
            {
                contentData = await loginResponseMessage.Content.ReadAsStringAsync();
                LoginResponse.AuthCode = loginResponseMessage.RequestMessage.RequestUri.AbsoluteUri.Substring(loginResponseMessage.RequestMessage.RequestUri.AbsoluteUri.IndexOf("=") + 1);
                LoginResponse.AuthCode = LoginResponse.AuthCode.Substring(0, LoginResponse.AuthCode.IndexOf('&'));
            }

            return loginResponseMessage;
        }

        protected async Task<HttpResponseMessage> GetMainPageAsync()
        {

            HttpClient.ClearRequestHeaders();
            var mainPageResponseMessage = new HttpResponseMessage();

            mainPageResponseMessage = await HttpClient.GetAsync(Resources.Home);

            return mainPageResponseMessage;
        }

        protected string GetGameSku(Platform platform)
        {
            switch (platform)
            {
                case Platform.Ps3:
                    return $"{Resources.GameSku}PS3";
                case Platform.Ps4:
                    return $"{Resources.GameSku}PS4";
                case Platform.Xbox360:
                    return $"{Resources.GameSku}XBX";
                case Platform.XboxOne:
                    return $"{Resources.GameSku}XBO";
                case Platform.Pc:
                    return $"{Resources.GameSku}PCC";
                case Platform.Switch:
                    return $"{Resources.GameSku}SWI";
                default:
                    throw new ArgumentOutOfRangeException(nameof(platform));
            }
        }

        protected static string GetPlatform(Platform platform)
        {
            switch (platform)
            {
                case Platform.Ps3:
                case Platform.Ps4:
                    return "ps3";
                case Platform.Xbox360:
                case Platform.XboxOne:
                    return "360";
                case Platform.Pc:
                    return "pc";
                case Platform.Switch:
                    return "swi";
                default:
                    throw new ArgumentOutOfRangeException(nameof(platform));
            }
        }
    }
}