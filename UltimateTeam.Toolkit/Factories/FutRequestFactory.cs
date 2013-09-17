using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Services;

namespace UltimateTeam.Toolkit.Factories
{
    internal class FutRequestFactory : IFutRequestFactory
    {
        private IHasher _hasher;

        public IHasher Hasher
        {
            get { return _hasher ?? (_hasher = new Hasher()); }
            set { _hasher = value; }
        }

        public async Task CreateLoginRequest(LoginDetails loginDetails)
        {
            var messageHandler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            var httpClient = new HttpClient(messageHandler);
            httpClient.DefaultRequestHeaders.ExpectContinue = false;
            var mainPageResponseMessage = await GetMainPageAsync(httpClient);
            await LoginAsync(loginDetails, httpClient, mainPageResponseMessage);
            var nucleusId = await GetNucleusIdAsync(httpClient);
            /*var shards = */ await GetShardsAsync(httpClient, nucleusId);
            var userAccounts = await GetUserAccountsAsync(httpClient);
            var sessionId = await GetSessionIdAsync(httpClient, nucleusId, userAccounts);
            await ValidateAsync(loginDetails, httpClient, sessionId);
        }

        private async Task ValidateAsync(LoginDetails loginDetails, HttpClient httpClient, string sessionId)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-UT-SID", sessionId);
            var validateResponseMessage = await httpClient.PostAsync("http://www.easports.com/iframe/fut/p/ut/game/fifa14/phishing/validate", new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("answer", Hasher.Hash(loginDetails.SecretAnswer))
                }));
            validateResponseMessage.EnsureSuccessStatusCode();
        }

        private static async Task<string> GetSessionIdAsync(HttpClient httpClient, string nucleusId, UserAccounts userAccounts)
        {
            var persona = userAccounts.UserAccountInfo.Personas
                .OrderByDescending(x => x.UserClubList.OrderByDescending(club => club.LastAccessDateTime))
                .First();
            var authResponseMessage = await httpClient.PostAsync("http://www.easports.com/iframe/fut/p/ut/auth", new StringContent(
                string.Format(@"{{ ""isReadOnly"": false, ""sku"": ""FUT14WEB"", ""clientVersion"": 1, ""nuc"": {0}, ""nucleusPersonaId"": {1}, ""nucleusPersonaDisplayName"": ""{2}"", ""nucleusPersonaPlatform"": ""{3}"", ""locale"": ""en-GB"", ""method"": ""authcode"", ""priorityLevel"":4, ""identification"": {{ ""authCode"": """" }} }}",
                    nucleusId, persona.PersonaId, persona.PersonaName, "ps3")));
            authResponseMessage.EnsureSuccessStatusCode();
            var sessionId = Regex.Match(await authResponseMessage.Content.ReadAsStringAsync(), "\"sid\":\"\\S+\"")
                .Value
                .Split(new[] { ':' })[1]
                .Replace("\"", string.Empty);
            return sessionId;
        }

        private static async Task<UserAccounts> GetUserAccountsAsync(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-UT-Route", "https://utas.s2.fut.ea.com:443");
            var accountInfoResponseMessage = await httpClient.GetAsync(string.Format("http://www.easports.com/iframe/fut/p/ut/game/fifa14/user/accountinfo?_={0}", CreateTimestamp()));
            accountInfoResponseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<UserAccounts>(await accountInfoResponseMessage.Content.ReadAsStringAsync());
        }

        private static async Task<Shards> GetShardsAsync(HttpClient httpClient, string nucleusId)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Easw-Session-Data-Nucleus-Id", nucleusId);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-UT-Embed-Error", "true");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-UT-Route", "https://utas.fut.ea.com");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json, text/javascript");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.8");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Referer", "http://www.easports.com/iframe/fut/?baseShowoffUrl=http%3A%2F%2Fwww.easports.com%2Fuk%2Ffifa%2Ffootball-club%2Fultimate-team%2Fshow-off&guest_app_uri=http%3A%2F%2Fwww.easports.com%2Fuk%2Ffifa%2Ffootball-club%2Fultimate-team&locale=en_GB");
            var shardsResponseMessage = await httpClient.GetAsync(string.Format("http://www.easports.com/iframe/fut/p/ut/shards?_={0}", CreateTimestamp()));
            shardsResponseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Shards>(await shardsResponseMessage.Content.ReadAsStringAsync());
        }

        private static async Task<string> GetNucleusIdAsync(HttpClient httpClient)
        {
            var nucleusResponseMessage = await httpClient.GetAsync("http://www.easports.com/iframe/fut/?locale=en_GB&baseShowoffUrl=http%3A%2F%2Fwww.easports.com%2Fuk%2Ffifa%2Ffootball-club%2Fultimate-team%2Fshow-off&guest_app_uri=http%3A%2F%2Fwww.easports.com%2Fuk%2Ffifa%2Ffootball-club%2Fultimate-team");
            nucleusResponseMessage.EnsureSuccessStatusCode();
            var nucleusId = Regex.Match(await nucleusResponseMessage.Content.ReadAsStringAsync(), "EASW_ID = '\\d+'")
                .Value
                .Split(new[] { " = " }, StringSplitOptions.RemoveEmptyEntries)[1]
                .Replace("'", string.Empty);
            return nucleusId;
        }

        private static async Task LoginAsync(LoginDetails loginDetails, HttpClient httpClient, HttpResponseMessage mainPageResponseMessage)
        {
            var loginResponseMessage = await httpClient.PostAsync(mainPageResponseMessage.RequestMessage.RequestUri, new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("email", loginDetails.Username),
                    new KeyValuePair<string, string>("password", loginDetails.Password),
                    new KeyValuePair<string, string>("_rememberMe", "on"),
                    new KeyValuePair<string, string>("rememberMe", "on"),
                    new KeyValuePair<string, string>("_eventId", "submit"),
                    new KeyValuePair<string, string>("facebookAuth", "")
                }));
            loginResponseMessage.EnsureSuccessStatusCode();
        }

        private static async Task<HttpResponseMessage> GetMainPageAsync(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.62 Safari/537.36");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip,deflate,sdch");
            var mainPageResponseMessage = await httpClient.GetAsync("http://www.easports.com/uk/fifa/football-club/ultimate-team");
            mainPageResponseMessage.EnsureSuccessStatusCode();
            return mainPageResponseMessage;
        }

        private static long CreateTimestamp()
        {
            var duration = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
            return ((long)(1000 * duration.TotalSeconds));
        }
    }
}