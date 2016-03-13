﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
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

        private string _machineKey = GetRandomHexNumber(16);
        private string _sessionId = string.Empty;
        private string _powSessionId = string.Empty;
        private string _nucUserId = string.Empty;
        private string _nucPersonaId = string.Empty;
        private string _code = string.Empty;

        public IHasher Hasher
        {
            get { return _hasher ?? (_hasher = new Hasher()); }
            set { _hasher = value; }
        }

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
                var mainPageResponseMessage = await GetMainPageAsync().ConfigureAwait(false);
                await LoginAsync(_loginDetails, mainPageResponseMessage);
                var authToken = await GetAuthTokenAsync(_code);
                var pid = await GetMobilePidAsync(authToken.Access_Token);
                _nucUserId = pid.Pid.ExternalRefValue;

                var sessionCode = await GetMobileAuthCodeAsync(authToken.Access_Token);
                var authCode = await GetMobileAuthCodeAsync(authToken.Access_Token);
                var powSessionId = await AuthPOWAsync(sessionCode.Code);
                _powSessionId = powSessionId.Sid;

                var nucleusId = await GetMobileNucleusIdAsync();
                var shards = await GetMobileShardsAsync();
                var userAccounts = await GetMobileUserAccountsAsync(_loginDetails.Platform);
                var sessionId = await AuthAsync(authCode.Code, nucleusId.UserData.Data.Where(n => n.Sku == GetGameSku(_loginDetails.Platform)).LastOrDefault().NucPersId, GetGameSku(_loginDetails.Platform));
                _sessionId = sessionId.Sid;

                var phishingToken = await ValidateAsync(_loginDetails);

                return new LoginResponse(_nucUserId, shards, userAccounts, _sessionId, phishingToken);
            }
            catch (Exception e)
            {
                throw new FutException("Unable to login", e);
            }
        }

        private async Task<AuthToken> GetAuthTokenAsync(string code)
        {
            AddMobileLoginHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.OriginFile, @"file://");
            AddContentHeader("application/x-www-form-urlencoded");
            var authTokenResponseMessage = await HttpClient.PostAsync(Resources.Token, new FormUrlEncodedContent(
                                                                                                                         new[]
                                                                                                                         {
                                                                                                                             new KeyValuePair<string, string>("grant_type", "authorization_code"),
                                                                                                                             new KeyValuePair<string, string>("code", code),
                                                                                                                             new KeyValuePair<string, string>("client_id", "FIFA-16-MOBILE-COMPANION"),
                                                                                                                             new KeyValuePair<string, string>("client_secret", "KrEoFK9ssvXKRWnTMgAu1OAMn7Y37ueUh1Vy7dIk2earFDUDABCvZuNIidYxxNbhwbj3y8pq6pSf8zBW"),
                                                                                                                         }));
            return await Deserialize<AuthToken>(authTokenResponseMessage);
        }

        private async Task<PidData> GetMobilePidAsync(string authCode)
        {
            AddMobileLoginHeaders();
            AddAuthorizationHeader(authCode);
            var PidDataResponseMessage = await HttpClient.GetAsync(string.Format(Resources.Pid));
            return await Deserialize<PidData>(PidDataResponseMessage);
        }

        private async Task<AuthCode> GetMobileAuthCodeAsync(string accessToken)
        {
            AddMobileLoginHeaders();
            var authTokenResponseMessage = await HttpClient.PostAsync(Resources.AuthCode, new FormUrlEncodedContent(
                                                                                                                         new[]
                                                                                                                         {
                                                                                                                             new KeyValuePair<string, string>("client_id", "FOS-SERVER"),
                                                                                                                             new KeyValuePair<string, string>("redirect_uri", "nucleus:rest"),
                                                                                                                             new KeyValuePair<string, string>("response_type", "code"),
                                                                                                                             new KeyValuePair<string, string>("access_token", accessToken),
                                                                                                                             new KeyValuePair<string, string>("machineProfileKey", _machineKey),
                                                                                                                         }));
            return await Deserialize<AuthCode>(authTokenResponseMessage);
        }

        
        private async Task<Auth> AuthPOWAsync(string AuthCode)
        {
            AddMobileLoginHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.OriginFile, @"file://");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.PowSessionId, string.Empty);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, string.Empty);
            var authMessage = await HttpClient.PostAsync(string.Format(Resources.POWAuth, CreateTimestamp()), new StringContent(
               string.Format(@"{{ ""isReadOnly"":true,""sku"":""FUT16AND"",""clientVersion"":18,""locale"":""en-GB"",""method"":""authcode"",""priorityLevel"":4,""identification"":{{""authCode"":""{0}"",""redirectUrl"":""nucleus:rest""}} }}", AuthCode)));
            var authResponse = await Deserialize<Auth>(authMessage);

            _powSessionId = authResponse.Sid;

            return authResponse;
        }

        private async Task<User> GetMobileNucleusIdAsync()
        {
            AddMobileLoginHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusId, _nucUserId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.PowSessionId, _powSessionId);
            var nucleusResponseMessage = await HttpClient.GetAsync(string.Format(Resources.NucleusId, _nucUserId, CreateTimestamp()));
            return await Deserialize<User>(nucleusResponseMessage);
        }

        private async Task<Shards> GetMobileShardsAsync()
        {
            AddMobileLoginHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusId, _nucUserId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, string.Empty);
            var shardsResponseMessage = await HttpClient.GetAsync(string.Format(Resources.Shards, CreateTimestamp()));
            return await Deserialize<Shards>(shardsResponseMessage);
        }

        private async Task<UserAccounts> GetMobileUserAccountsAsync(Platform platform)
        {
            AddMobileLoginHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusId, _nucUserId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, string.Empty);
            var accountInfoResponseMessage = await HttpClient.GetAsync(string.Format(Resources.AccountInfo, CreateTimestamp()));
            return await Deserialize<UserAccounts>(accountInfoResponseMessage);
        }

        private async Task<Auth> AuthAsync(string AuthCode, string PersonaId, string Sku)
        {
            AddMobileLoginHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.OriginFile, @"file://");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, string.Empty);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.PowSessionId, string.Empty);
            var authMessage = await HttpClient.PostAsync(string.Format(Resources.Auth, CreateTimestamp()), new StringContent(
               string.Format(@"{{ ""isReadOnly"":false,""sku"":""FUT16AND"",""clientVersion"":18,""locale"":""en-GB"",""method"":""authcode"",""priorityLevel"":4,""identification"":{{""authCode"":""{0}"",""redirectUrl"":""nucleus:rest""}},""nucleusPersonaId"":""{1}"",""gameSku"":""{2}"" }}", AuthCode, PersonaId, Sku)));
            var authResponse = await Deserialize<Auth>(authMessage);

            return authResponse;
        }

        private async Task<string> ValidateAsync(LoginDetails loginDetails)
        {
            AddMobileLoginHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.OriginFile, @"file://");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusId, _nucUserId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, _sessionId);
            var validateResponseMessage = await HttpClient.PostAsync(Resources.Validate, new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("answer", Hasher.Hash(loginDetails.SecretAnswer))
                }));
            var validateResponse = await Deserialize<ValidateResponse>(validateResponseMessage);

            return validateResponse.Token;
        }

        private static string GetGameSku(Platform platform)
        {
            switch (platform)
            {
                case Platform.Ps3:
                    return "FFA16PS3";
                case Platform.Ps4:
                    return "FFA16PS4";
                case Platform.Xbox360:
                    return "FFA16XBX";
                case Platform.XboxOne:
                    return "FFA16XBO";
                case Platform.Pc:
                    return "FFA16PCC";
                default:
                    throw new ArgumentOutOfRangeException(nameof(platform), platform, null);
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
                    throw new ArgumentOutOfRangeException("platform");
            }
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

            //check if twofactorcode is required
            var contentData = await loginResponseMessage.Content.ReadAsStringAsync();
            if (contentData.Contains("We sent a security code to your") || contentData.Contains("Your security code was sent to") || contentData.Contains("Enter the 6-digit verification code generated by your App Authenticator") || contentData.Contains("Enter the 6-digit verification code generated by your App Authenticator"))
                await SetTwoFactorCodeAsync(loginResponseMessage);
            
            //TO DO: How to verify, that content is a companion code
            else if (contentData.Length <= 64)
            {
                _code = contentData;
            }
        }

        private async Task SetTwoFactorCodeAsync(HttpResponseMessage loginResponse)
        {
            var tfCode = await _twoFactorCodeProvider.GetTwoFactorCodeAsync();

            var responseContent = await loginResponse.Content.ReadAsStringAsync();

            AddReferrerHeader(loginResponse.RequestMessage.RequestUri.ToString());

            var codeResponseMessage = await HttpClient.PostAsync(loginResponse.RequestMessage.RequestUri, new FormUrlEncodedContent(
                        new[]
                            {
                            new KeyValuePair<string, string>(responseContent.Contains("twofactorCode") ? "twofactorCode" : "twoFactorCode", tfCode),
                            new KeyValuePair<string, string>("_eventId", "submit"),
                            new KeyValuePair<string, string>("_trustThisDevice", "on"),
                            new KeyValuePair<string, string>("trustThisDevice", "on")
                            }));

            codeResponseMessage.EnsureSuccessStatusCode();

            var contentData = await codeResponseMessage.Content.ReadAsStringAsync();

            if (contentData.Contains("Incorrect code entered"))
                throw new FutException("Incorrect TwoFactorCode entered.");

            if (contentData.Contains("Tired of waiting for your code?"))
                await SkipAuthenticatorAdvertise(codeResponseMessage);

            //TO DO: How to verify, that content is a companion code
            else if (contentData.Length <= 64)
            {
                _code = contentData;
            }
        }

        private async Task SkipAuthenticatorAdvertise(HttpResponseMessage codeResponse)
        {
            var responseContent = await codeResponse.Content.ReadAsStringAsync();

            AddReferrerHeader(codeResponse.RequestMessage.RequestUri.ToString());

            var authenticatorResponseMessage = await HttpClient.PostAsync(codeResponse.RequestMessage.RequestUri, new FormUrlEncodedContent(
                        new[]
                            {
                            new KeyValuePair<string, string>("_eventId", "cancel"),
                            new KeyValuePair<string, string>("appDevice", "ANDROID"),
                            }));

            authenticatorResponseMessage.EnsureSuccessStatusCode();

            var contentData = await authenticatorResponseMessage.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> GetMainPageAsync()
        {
            AddUserAgent();
            AddAcceptEncodingHeader();
            var mainPageResponseMessage = await HttpClient.GetAsync(Resources.Home);
            mainPageResponseMessage.EnsureSuccessStatusCode();

            //check if twofactorcode is required
            var contentData = await mainPageResponseMessage.Content.ReadAsStringAsync();
            if (contentData.Contains("We sent a security code to your") || contentData.Contains("Your security code was sent to") || contentData.Contains("Enter the 6-digit verification code generated by your App Authenticator"))
                await SetTwoFactorCodeAsync(mainPageResponseMessage);

            return mainPageResponseMessage;
        }

        private static long CreateTimestamp()
        {
            var duration = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);

            return ((long)(1000 * duration.TotalSeconds));
        }

        static Random random = new Random();
        public static string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }
    }
}