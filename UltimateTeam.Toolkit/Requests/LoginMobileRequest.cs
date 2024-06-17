using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Services;

namespace UltimateTeam.Toolkit.Requests
{
    internal class LoginRequestMobile : FutRequestBase, IFutRequest<LoginResponse>
    {
        private readonly LoginDetails _loginDetails;
        private readonly ITwoFactorCodeProvider _twoFactorCodeProvider;
        private IHasher _hasher;

        private static readonly Random Random = new Random();
        private string _sessionId = string.Empty;
        private string _route;

        private MobileToken _token;

        private IHasher Hasher => _hasher ?? (_hasher = new Hasher());

        public LoginRequestMobile(LoginDetails loginDetails, ITwoFactorCodeProvider twoFactorCodeProvider)
        {
            loginDetails.ThrowIfNullArgument();
            _loginDetails = loginDetails;
            _twoFactorCodeProvider = twoFactorCodeProvider;
        }

        public void SetCookieContainer(CookieContainer cookieContainer)
        {
            HttpClient.MessageHandler.CookieContainer = cookieContainer;
        }



        public async Task<LoginResponse> PerformRequestAsync()
        {
            try
            {
                var httpResponseMessage = await GetMainPageAsync().ConfigureAwait(false);
                if (httpResponseMessage.Content.ReadAsStringAsync().Result.Contains("<title>Log In</title>"))
                {
                    httpResponseMessage = await LoginAsync(_loginDetails, httpResponseMessage);
                }
                httpResponseMessage = await HandleAccountUpdate(httpResponseMessage);
                httpResponseMessage = await SetTwoFactorCodeAsync(httpResponseMessage);
                httpResponseMessage = await CancelUpdateAuthenticationModeAsync(httpResponseMessage);
                _token = await GetToken(httpResponseMessage);
                var nucleusId = await GetMobileNucleusIdAsync();
                var shards = await GetMobileShardsAsync(nucleusId);
                var userAccounts = await GetMobileUserAccountsAsync(_loginDetails.Platform);
                _sessionId = await GetSessionIdAsync(userAccounts, _loginDetails.Platform);
                var phishingToken = await ValidateAsync(_loginDetails, _sessionId);

                return new LoginResponse(nucleusId, shards, userAccounts, _sessionId, phishingToken, nucleusId);
            }
            catch (Exception e)
            {
                throw new FutException($"Unable to login to {AppVersion}", e);
            }
        }

        private static string GetNucleusPersonaPlatform(Platform platform)
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(platform));
            }
        }

        private static string GetgameSkuPlatform(Platform platform)
        {
            switch (platform)
            {
                case Platform.Ps3:
                    return "FFA17PS3";
                case Platform.Ps4:
                    return "FFA17PS4";
                case Platform.Xbox360:
                    return "FFA17XBX";
                case Platform.XboxOne:
                    return "FFA17XBO";
                case Platform.Pc:
                    return "FFA17PCC";
                default:
                    throw new ArgumentOutOfRangeException(nameof(platform));
            }
        }

        private async Task<string> GetSessionIdAsync(UserAccounts userAccounts, Platform platform)
        {
            HttpResponseMessage httpResponse = await HttpClient.GetAsync(string.Format("https://accounts.ea.com/connect/auth?client_id=FOS-SERVER&redirect_uri=nucleus:rest&response_type=code&access_token={0}", _token.AccessToken));
            httpResponse.EnsureSuccessStatusCode();
            string code = (await DeserializeAsync<MobileToken>(httpResponse)).Code;
            var persona = userAccounts
                .UserAccountInfo
                .Personas
                .FirstOrDefault(p => p.UserClubList.Any(club => club.Platform == GetNucleusPersonaPlatform(platform) && club.Year.Contains("2017")))
                ;
            if (persona == null)
            {
                throw new FutException("Couldn't find a persona matching the selected platform");
            }
            var httpResponseMessage = await HttpClient.PostAsync(string.Format("{0}/ut/auth?timestamp={1}", _route, CreateTimestamp()), new StringContent(string.Format("{{ \"isReadOnly\": false, \"sku\": \"FUT17AND\", \"clientVersion\": 22, \"locale\": \"en-US\", \"method\": \"authcode\", \"priorityLevel\":4, \"identification\": {{ \"authCode\": \"{1}\",\"redirectUrl\":\"nucleus:rest\" }}, \"nucleusPersonaId\": {0},\"gameSku\": \"{2}\" }}", persona.PersonaId, code, GetgameSkuPlatform(platform))));
            httpResponseMessage.EnsureSuccessStatusCode();
            await httpResponseMessage.Content.ReadAsStringAsync();
            var sessionAuthResponse = await DeserializeAsync<SessionAuthResponse>(httpResponseMessage);
            var result = sessionAuthResponse.Sid;
            return result;
        }

        private async Task<MobileToken> GetToken(HttpResponseMessage response)
        {
            var str = response.RequestMessage.RequestUri.Query.Replace("?code=", "");
            var httpResponseMessage = await HttpClient.PostAsync("https://accounts.ea.com/connect/token?grant_type=authorization_code&code=" + str + "&client_id=FIFA-17-MOBILE-COMPANION&client_secret=qIdl15XHu4VWrcolro37Um0JiuRjnbIspnYtXmv3zr6pPL9S0N9H1IutSFJvnCORH3isebmeVdCtHaFC", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", "")
            }));
            response = httpResponseMessage;
            response.EnsureSuccessStatusCode();
            return await DeserializeAsync<MobileToken>(response);
        }

        private async Task<string> GetMobileNucleusIdAsync()
        {
            AddAuthorizeHeader(_token.TokenType, _token.AccessToken);
            var response = await HttpClient.GetAsync("https://gateway.ea.com/proxy/identity/pids/me");
            response.EnsureSuccessStatusCode();
            var pidId = (await DeserializeAsync<MobileToken>(response)).Pid.PidId;
            RemoveAuthorizeHeader();
            return pidId;
        }

        private async Task<Shards> GetMobileShardsAsync(string nucleusId)
        {
            HttpClient.AddRequestHeader("Easw-Session-Data-Nucleus-Id", nucleusId);
            HttpClient.AddRequestHeader("X-UT-Embed-Error", "true");
            HttpClient.AddRequestHeader("X-Requested-With", "XMLHttpRequest");
            AddAcceptHeader("application/json, text/javascript");
            AddAcceptLanguageHeader();
            var httpResponseMessage = await HttpClient.GetAsync(string.Format("https://utas.mob.v1.fut.ea.com/ut/shards/v2?_={0}", CreateTimestamp()));
            var httpResponseMessage2 = httpResponseMessage;
            await httpResponseMessage2.Content.ReadAsStringAsync();
            return await DeserializeAsync<Shards>(httpResponseMessage2);
        }

        private static long CreateTimestamp()
        {
            return (long)(1000.0 * (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
        }

        private async Task<UserAccounts> GetMobileUserAccountsAsync(Platform platform)
        {
            HttpClient.RemoveRequestHeader("X-UT-Route");
            _route = string.Format("https://utas.external.{0}fut.ea.com:443", (platform == Platform.Xbox360) ? "s3." : ((platform == Platform.XboxOne) ? "s3." : ((platform == Platform.Ps3) ? "s2." : ((platform == Platform.Ps4) ? "s2." : ((platform == Platform.Pc) ? "s2." : string.Empty)))));
            return await DeserializeAsync<UserAccounts>(await HttpClient.GetAsync(string.Format("{1}/ut/game/fifa17/user/accountinfo?filterConsoleLogin=true&sku=FUT17AND_={0}", CreateTimestamp(), _route)));
        }

        private async Task<string> ValidateAsync(LoginDetails loginDetails, string sessionId)
        {
            HttpClient.AddRequestHeader("X-UT-SID", sessionId);
            return (await DeserializeAsync<ValidateResponse>(await HttpClient.PostAsync(string.Format("{0}/ut/game/fifa17/phishing/validate?answer={1}&timestamp={2}", _route, Hasher.Hash(loginDetails.SecretAnswer), CreateTimestamp()), new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("answer", Hasher.Hash(loginDetails.SecretAnswer))
            })))).Token;
        }

        private async Task<HttpResponseMessage> LoginAsync(LoginDetails loginDetails, HttpResponseMessage mainPageResponseMessage)
        {
            var loginResponseMessage = await HttpClient.PostAsync(mainPageResponseMessage.RequestMessage.RequestUri, new FormUrlEncodedContent(
                                                                                                                         new[]
                                                                                                                         {
                                                                                                                             new KeyValuePair<string, string>("email", loginDetails.Username),
                                                                                                                             new KeyValuePair<string, string>("password", loginDetails.Password),
                                                                                                                             new KeyValuePair<string, string>("_rememberMe", "on"),
                                                                                                                             new KeyValuePair<string, string>("rememberMe", "on"),
                                                                                                                             new KeyValuePair<string, string>("_eventId", "submit"),
                                                                                                                             new KeyValuePair<string, string>("prompt", "login")
                                                                                                                         }));
            loginResponseMessage.EnsureSuccessStatusCode();


            var contentData = await loginResponseMessage.Content.ReadAsStringAsync();
            //Redirect
            var redirectPage = await HttpClient.GetAsync(Regex.Match(contentData, "https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{2,256}\\.[a-z]{2,6}\\b([-a-zA-Z0-9@:%_\\+.~#?&//=]*)").Value + "&_eventId=end");
            await redirectPage.Content.ReadAsStringAsync();
            return redirectPage;

        }

        private async Task<HttpResponseMessage> HandleAccountUpdate(HttpResponseMessage response)
        {
            HttpResponseMessage result;
            if (!(await response.Content.ReadAsStringAsync()).Contains("Account Update"))
            {
                result = response;
            }
            else
            {
                response = await HttpClient.PostAsync(response.RequestMessage.RequestUri, new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("_eventId", "submit")
                }));
                response.EnsureSuccessStatusCode();
                if (response.Content.ReadAsStringAsync().Result.Contains("Email my code"))
                {
                    response = await HttpClient.PostAsync(response.RequestMessage.RequestUri, new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("twofactorType", "EMAIL"),
                        new KeyValuePair<string, string>("_eventId", "submit")
                    }));
                    response.EnsureSuccessStatusCode();
                }
                result = response;
            }
            return result;
        }

        private async Task<HttpResponseMessage> CancelUpdateAuthenticationModeAsync(HttpResponseMessage response)
        {
            HttpResponseMessage result;
            if (!(await response.Content.ReadAsStringAsync()).Contains("Set Up an App Authenticator"))
            {
                result = response;
            }
            else
            {
                response = await HttpClient.PostAsync(response.RequestMessage.RequestUri, new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("_eventId", "cancel"),
                    new KeyValuePair<string, string>("appDevice", "IPHONE")
                }));
                response.EnsureSuccessStatusCode();
                result = response;
            }
            return result;
        }

        private async Task<HttpResponseMessage> SetTwoFactorCodeAsync(HttpResponseMessage loginResponse)
        {
            HttpResponseMessage result;
            if (loginResponse == null)
            {
                result = null;
            }
            else
            {
                var text = await loginResponse.Content.ReadAsStringAsync();
                if (!text.Contains("We sent a security code to your") && !text.Contains("Your security code was sent to") && !text.Contains("Enter the 6-digit verification code generated by your App Authenticator"))
                {
                    result = loginResponse;
                }
                else
                {
                    //var twoFactorCode = await _twoFactorCodeProvider.GetTwoFactorCodeAsync(_loginDetails.Username);
                    var twoFactorCode = await _twoFactorCodeProvider.GetTwoFactorCodeAsync();
                    var loginResponseContent = await loginResponse.Content.ReadAsStringAsync();
                    AddReferrerHeader(loginResponse.RequestMessage.RequestUri.ToString());
                    HttpResponseMessage httpResponseMessage = await HttpClient.PostAsync(loginResponse.RequestMessage.RequestUri, new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>(loginResponseContent.Contains("twofactorCode") ? "twofactorCode" : "twoFactorCode", twoFactorCode),
                        new KeyValuePair<string, string>("_eventId", "submit"),
                        new KeyValuePair<string, string>("_trustThisDevice", "on"),
                        new KeyValuePair<string, string>("trustThisDevice", "on")
                    }));
                    httpResponseMessage.EnsureSuccessStatusCode();
                    if ((await httpResponseMessage.Content.ReadAsStringAsync()).Contains("Incorrect code entered"))
                    {
                        throw new FutException("Incorrect TwoFactorCode entered.");
                    }
                    result = httpResponseMessage;
                }
            }
            return result;
        }
        private async Task<HttpResponseMessage> GetMainPageAsync()
        {
            AddMobileUserAgent();
            AddAcceptEncodingHeader();
            var mainPageResponseMessage = await HttpClient.GetAsync(Resources.Home);
            mainPageResponseMessage.EnsureSuccessStatusCode();

            //check if twofactorcode is required
            var contentData = await mainPageResponseMessage.Content.ReadAsStringAsync();
            if (contentData.Contains("We sent a security code to your") ||
                contentData.Contains("Your security code was sent to") ||
                contentData.Contains("Enter the 6-digit verification code generated by your App Authenticator"))
            {
                await SetTwoFactorCodeAsync(mainPageResponseMessage);
            }

            return mainPageResponseMessage;
        }

        public static string GetRandomHexNumber(int digits)
        {
            var buffer = new byte[digits / 2];
            Random.NextBytes(buffer);
            var result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + Random.Next(16).ToString("X");
        }

        private void AddAuthorizeHeader(string type, string token)
        {
            HttpClient.AddRequestHeader("Authorization", type + " " + token);
            HttpClient.AddRequestHeader("Proxy-Connection", "keep-alive");
        }

        private void RemoveAuthorizeHeader()
        {
            HttpClient.RemoveRequestHeader("Authorization");
            HttpClient.RemoveRequestHeader("Proxy-Connection");
        }
    }
}