using System.Text.RegularExpressions;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Models.Auth;
using UltimateTeam.Toolkit.Models.Generic;
using UltimateTeam.Toolkit.RequestFactory;
using UltimateTeam.Toolkit.Services;

namespace UltimateTeam.Toolkit.Requests
{
    internal class LoginRequest : FutRequestBase, IFutRequest<LoginResponse>
    {
        private ITwoFactorCodeProvider _twoFactorCodeProvider;
        private AuthenticationType _authType;

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

                //Get the AccessToken
                var loginResponseMessage = await LoginAsync(mainPageResponseMessage);
                if (string.IsNullOrEmpty(LoginResponse.AccessToken))
                    throw new FutException("No AccessToken received.");

                //GetPersonId & NucleusId
                LoginResponse.PersonId = await GetPidDataAsync(LoginResponse.AccessToken);
                LoginResponse.NucleusId = LoginResponse.PersonId.Pid.ExternalRefValue;

                //Get Personas & Clubs
                var userAccountsAuthCode = await GetAuthCodeAsync(LoginResponse.AccessToken, "shard5");
                LoginResponse.UserAccounts = await GetUserAccountsAsync(userAccountsAuthCode);
                LoginResponse.DefaultPersona = MatchDefaultPersona(LoginResponse.UserAccounts);

                var utAuthCode = await GetAuthCodeAsync(LoginResponse.AccessToken, "ut-auth");
                LoginResponse.AuthData = await AuthAsync(utAuthCode);

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
            var pidDataResponseMessage = await HttpClient.GetAsync(string.Format(Resources.Pids));
            var pidData = await DeserializeAsync<PidData>(pidDataResponseMessage);

            if (pidData == null || pidData.Pid == null)
                throw new Exception($"Got no PID Data during the Loginprocess to to {LoginDetails?.AppVersion}.");
            return pidData;
        }

        private async Task<string> GetAuthCodeAsync(string accessToken, string clientSequence)
        {
            AddLoginHeaders();
            var authCodeResponseMessage = await HttpClient.GetAsync(string.Format(Resources.AuthCode, accessToken, clientSequence));
            var authCode = await DeserializeAsync<AuthCode>(authCodeResponseMessage);

            if (authCode == null || authCode.Code == null)
                throw new Exception($"Got no AuthCode during the Loginprocess to {LoginDetails?.AppVersion}.");

            return authCode.Code;
        }

        protected async Task<UserAccounts> GetUserAccountsAsync(string authCode)
        {
            AddLoginHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusId, LoginResponse.NucleusId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusAccessCode, authCode);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusRedirectUrl, "nucleus:rest");
            HttpClient.AddRequestHeader("Content-Type", "application/json");



            var accountInfoResponseMessage = await HttpClient.GetAsync(Resources.UserAccounts);
            var accountInfoResponseMessageContent = await accountInfoResponseMessage.Content.ReadAsStringAsync();
            var userAccounts = await DeserializeAsync<UserAccounts>(accountInfoResponseMessage);

            if (userAccounts?.UserAccountInfo?.Personas == null || userAccounts.UserAccountInfo.Personas.Count() <= 0)
            {
                throw new Exception($"Unable to get Personas {LoginDetails?.AppVersion}.");
            }
            return userAccounts;
        }

        private Persona MatchDefaultPersona(UserAccounts userAccounts)
        {
            var matchingPersona = new Persona();
            try
            {
                matchingPersona = LoginResponse.UserAccounts.UserAccountInfo.Personas.Where(p => p.UserClubList.Any(uc => uc.Year == Resources.CurrentYearLong.ToString() &&
                                                                                                                    uc.SkuAccessList.ContainsKey(GetGameSku(LoginDetails.Platform)))).FirstOrDefault();
                if (matchingPersona == null)
                    throw new FutException($"Unable to match a valid Persona for {LoginDetails?.AppVersion}.");
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to match a valid Persona for {LoginDetails?.AppVersion}.", e);
            }
            return matchingPersona;
        }

        protected async Task<AuthInfo> AuthAsync(string utAuthCode)
        {
            string httpContent;
            var authResponseMessage = new HttpResponseMessage();

            AddLoginHeaders();
            httpContent = $@"{{""clientVersion"":{Resources.ClientVersion},""gameSku"":""{GetGameSku(LoginDetails.Platform)}"",""identification"":{{""authCode"":""{utAuthCode}"",""redirectUrl"":""nucleus:rest""}},""isReadOnly"":false,""locale"":""en-US"",""method"":""authcode"",""nucleusPersonaId"":{LoginResponse.DefaultPersona.PersonaId},""priorityLevel"":4,""sku"":""{Resources.Sku}""}}";
            authResponseMessage = await HttpClient.PostAsync(Resources.Auth, new StringContent(httpContent));

            var authResponse = await DeserializeAsync<AuthInfo>(authResponseMessage);
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
                                                                                                                            new KeyValuePair<string, string>("loginMethod", "emailPassword"),
                                                                                                                            new KeyValuePair<string, string>("_eventId", "submit"),
                                                                                                                            new KeyValuePair<string, string>("cid", ""),
                                                                                                                            new KeyValuePair<string, string>("regionCode", "DE"),
                                                                                                                            new KeyValuePair<string, string>("phoneNumber", ""),
                                                                                                                            new KeyValuePair<string, string>("passwordForPhone", ""),
                                                                                                                            new KeyValuePair<string, string>("_rememberMe", "on"),
                                                                                                                            new KeyValuePair<string, string>("rememberMe", "on"),
                                                                                                                            new KeyValuePair<string, string>("thirdPartyCaptchaResponse", ""),
                                                                                                                            new KeyValuePair<string, string>("isPhoneNumberLogin", "false"),
                                                                                                                            new KeyValuePair<string, string>("isIncompletePhone", ""),
                                                                                                                            new KeyValuePair<string, string>("showAgeUp", "true")
                                                                                                                        }));
            contentData = await loginResponseMessage.Content.ReadAsStringAsync();

            if (contentData.Contains("Your credentials are incorrect or have expired") || contentData.Contains("Email address is invalid"))
            {
                throw new Exception($"Wrong credentials for {LoginDetails?.AppVersion}.");
            }


            if (contentData.Contains("Verify your identity"))
            {
                loginResponseMessage = await SetTwoFactorCodeAsync(loginResponseMessage);
                contentData = await loginResponseMessage.Content.ReadAsStringAsync();
            }

            string pattern = @"window\.location\s*=\s*['""](?<url>https?://[^'""]+)['""]";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(contentData);
            if (match.Success)
            {
                string redirectUrl = match.Groups["url"].Value;
                loginResponseMessage = await LoginForwarder(redirectUrl);
            }

            if (loginResponseMessage.RequestMessage.RequestUri.AbsoluteUri.Contains("access_token="))
            {
                loginResponseMessage = await LoginForwarder(loginResponseMessage.RequestMessage.RequestUri.ToString());
                contentData = await loginResponseMessage.Content.ReadAsStringAsync();
                LoginResponse.AccessToken = loginResponseMessage.RequestMessage.RequestUri.AbsoluteUri.Substring(loginResponseMessage.RequestMessage.RequestUri.AbsoluteUri.IndexOf("=") + 1);
                LoginResponse.AccessToken = LoginResponse.AccessToken.Substring(0, LoginResponse.AccessToken.IndexOf('&'));
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

        protected async Task<HttpResponseMessage> LoginForwarder(string requestUri)
        {
            HttpResponseMessage forwardResponseMessage = new HttpResponseMessage();

            forwardResponseMessage = await HttpClient.GetAsync(requestUri);
            return forwardResponseMessage;
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
                                                                                                                            new KeyValuePair<string, string>("codeType", "EMAIL"),
                                                                                                                            new KeyValuePair<string, string>("maskedDestination", string.Concat(LoginDetails.Username.Substring(0, 2), new string('*', LoginDetails.Username.IndexOf('@') - 2), "%40", LoginDetails.Username.Split('@')[1]))
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
            _authType = AuthenticationType.Email;

            string contentData = await loginResponse.Content.ReadAsStringAsync();
            loginResponse = await LoginForwarder(loginResponse.RequestMessage.RequestUri.ToString());
            contentData = await loginResponse.Content.ReadAsStringAsync();

            HttpResponseMessage sended = await SetTwoFactorTypeAsync(loginResponse);
            loginResponse = await LoginForwarder(sended.RequestMessage.RequestUri.ToString());

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

            if (_authType == AuthenticationType.Unknown)
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

        protected string GetGameSku(Platform platform)
        {
            switch (platform)
            {
                case Platform.Ps3:
                    return $"{Resources.GameSku}PS3";
                case Platform.Ps4:
                    return $"{Resources.GameSku}PS4";
                case Platform.Ps5:
                    return $"{Resources.GameSku}PS5";
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
    }
}