using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Services;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class LoginRequest : FutRequestBase, IFutRequest<LoginResponse>
    {
        private readonly LoginDetails _loginDetails;

        private IHasher _hasher;

        public IHasher Hasher
        {
            get { return _hasher ?? (_hasher = new Hasher()); }
            set { _hasher = value; }
        }

        public LoginRequest(LoginDetails loginDetails)
        {
            loginDetails.ThrowIfNullArgument();
            _loginDetails = loginDetails;
        }

        public void SetCookieContainer(CookieContainer cookieContainer)
        {
            HttpClient.MessageHandler.CookieContainer = cookieContainer;
        }

        public async Task<LoginResponse> PerformRequestAsync()
        {
            try
            {
                var mainPageResponseMessage = await GetMainPageAsync().ConfigureAwait(false);
                await LoginAsync(_loginDetails, mainPageResponseMessage);
                var nucleusId = await GetNucleusIdAsync();
                var shards = await GetShardsAsync(nucleusId);
                var userAccounts = await GetUserAccountsAsync(_loginDetails.Platform);
                var sessionId = await GetSessionIdAsync(nucleusId, userAccounts, _loginDetails.Platform);
                var phishingToken = await ValidateAsync(_loginDetails, sessionId);

                return new LoginResponse(nucleusId, shards, userAccounts, sessionId, phishingToken);
            }
            catch (Exception e)
            {
                throw new FutException("Unable to login", e);
            }
        }

        private async Task<string> ValidateAsync(LoginDetails loginDetails, string sessionId)
        {
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, sessionId);
            var validateResponseMessage = await HttpClient.PostAsync(Resources.Validate, new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("answer", Hasher.Hash(loginDetails.SecretAnswer))
                }));
            var validateResponse = await Deserialize<ValidateResponse>(validateResponseMessage);

            return validateResponse.Token;
        }

        private async Task<string> GetSessionIdAsync(string nucleusId, UserAccounts userAccounts, Platform platform)
        {
            var persona = userAccounts
                .UserAccountInfo
                .Personas
                .FirstOrDefault(p => p.UserClubList.Any(club => club.Platform == GetNucleusPersonaPlatform(platform)));
            if (persona == null)
            {
                throw new FutException("Couldn't find a persona matching the selected platform");
            }
            var authResponseMessage = await HttpClient.PostAsync(Resources.Auth, new StringContent(
                string.Format(@"{{ ""isReadOnly"": false, ""sku"": ""FUT14WEB"", ""clientVersion"": 1, ""nuc"": {0}, ""nucleusPersonaId"": {1}, ""nucleusPersonaDisplayName"": ""{2}"", ""nucleusPersonaPlatform"": ""{3}"", ""locale"": ""en-GB"", ""method"": ""authcode"", ""priorityLevel"":4, ""identification"": {{ ""authCode"": """" }} }}",
                    nucleusId, persona.PersonaId, persona.PersonaName, GetNucleusPersonaPlatform(platform))));
            authResponseMessage.EnsureSuccessStatusCode();
            var sessionId = Regex.Match(await authResponseMessage.Content.ReadAsStringAsync(), "\"sid\":\"\\S+\"")
                .Value
                .Split(new[] { ':' })[1]
                .Replace("\"", string.Empty);

            return sessionId;
        }

        private static string GetNucleusPersonaPlatform(Platform platform)
        {
            switch (platform)
            {
                case Platform.Ps3:
                    return "ps3";
                case Platform.Xbox360:
                    return "360";
                case Platform.Pc:
                    return "pc";
                default:
                    throw new ArgumentOutOfRangeException("platform");
            }
        }

        private async Task<UserAccounts> GetUserAccountsAsync(Platform platform)
        {
            HttpClient.RemoveRequestHeader(NonStandardHttpHeaders.Route);
            var route = string.Format("https://utas.{0}fut.ea.com:443", platform == Platform.Xbox360 ? string.Empty : "s2.");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.Route, route);
            var accountInfoResponseMessage = await HttpClient.GetAsync(string.Format(Resources.AccountInfo, CreateTimestamp()));

            return await Deserialize<UserAccounts>(accountInfoResponseMessage);
        }

        private async Task<Shards> GetShardsAsync(string nucleusId)
        {
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusId, nucleusId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.EmbedError, "true");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.Route, "https://utas.fut.ea.com");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.RequestedWith, "XMLHttpRequest");
            AddAcceptHeader("application/json, text/javascript");
            AddAcceptLanguageHeader();
            AddReferrerHeader(Resources.BaseShowoff);
            var shardsResponseMessage = await HttpClient.GetAsync(string.Format(Resources.Shards, CreateTimestamp()));

            return await Deserialize<Shards>(shardsResponseMessage);
        }

        private async Task<string> GetNucleusIdAsync()
        {
            var nucleusResponseMessage = await HttpClient.GetAsync(Resources.NucleusId);
            nucleusResponseMessage.EnsureSuccessStatusCode();
            var nucleusId = Regex.Match(await nucleusResponseMessage.Content.ReadAsStringAsync(), "EASW_ID = '\\d+'")
                .Value
                .Split(new[] { " = " }, StringSplitOptions.RemoveEmptyEntries)[1]
                .Replace("'", string.Empty);

            return nucleusId;
        }

        private async Task LoginAsync(LoginDetails loginDetails, HttpResponseMessage mainPageResponseMessage)
        {
            var loginResponseMessage = await HttpClient.PostAsync(mainPageResponseMessage.RequestMessage.RequestUri, new FormUrlEncodedContent(
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

        private async Task<HttpResponseMessage> GetMainPageAsync()
        {
            AddUserAgent();
            AddAcceptEncodingHeader();
            var mainPageResponseMessage = await HttpClient.GetAsync(Resources.Home);
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