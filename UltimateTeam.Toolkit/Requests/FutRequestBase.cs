using Newtonsoft.Json;
using System.Net;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Models.Auth;
using UltimateTeam.Toolkit.Models.Generic;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    public abstract class FutRequestBase
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error };
        private IHttpClient _httpClient;
        private LoginDetails _loginDetails = new LoginDetails();
        private LoginResponse _loginResponse = new LoginResponse();

        protected AppVersion _appVersion;

        public LoginResponse LoginResponse
        {
            set
            {
                _loginResponse = value;
            }
            get
            {
                return _loginResponse;
            }
        }

        public LoginDetails LoginDetails
        {
            set
            {
                _loginDetails = value;
            }
            get
            {
                return _loginDetails;
            }
        }

        internal Resources Resources { get; set; }

        internal IHttpClient HttpClient
        {
            get { return _httpClient; }
            set
            {
                value.ThrowIfNullArgument();
                _httpClient = value;
            }
        }

        protected void AddCommonHeaders()
        {
            if (LoginResponse?.NucleusId == null)
            {
                throw new Exception($"Got no Nucleus Data and Auth Data during the Loginprocess {LoginDetails?.AppVersion}.");
            }

            HttpClient.ClearRequestHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, LoginResponse.AuthData.Sid);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.Origin, @"https://www.ea.com");
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
            AddAcceptHeader("text/plain, */*; q=0.01");
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, "application/json");
            AddUserAgent();
            AddRefererHeader("https://www.ea.com/");
            HttpClient.AddConnectionKeepAliveHeader();
        }

        protected void AddAnonymousHeader(string acceptHeader)
        {
            HttpClient.ClearRequestHeaders();
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
            AddAcceptHeader(acceptHeader);
            AddUserAgent();
            AddRefererHeader("https://www.ea.com/ea-sports-fc/ultimate-team/web-app/");
            HttpClient.AddConnectionKeepAliveHeader();
        }

        protected void AddLoginHeaders()
        {
            HttpClient.ClearRequestHeaders();
            HttpClient.AddConnectionKeepAliveHeader();
            AddAcceptHeader("*/*");
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, "application/json");
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.Origin, @"https://www.ea.com");
            AddRefererHeader("https://www.ea.com/");
            AddUserAgent();
        }

        protected void AddContentHeader(string contentType)
        {
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, contentType);
        }

        protected void AddEncodingHeader(string encodingType)
        {
            HttpClient.AddRequestHeader(HttpHeaders.AcceptEncoding, encodingType);
        }

        protected void AddUserAgent()
        {
            HttpClient.AddRequestHeader(HttpHeaders.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
        }

        protected void AddAcceptHeader(string value)
        {
            HttpClient.AddRequestHeader(HttpHeaders.Accept, value);
        }

        protected void AddRefererHeader(string value)
        {
            HttpClient.SetReferrerUri(value);
        }

        protected void AddAcceptEncodingHeader()
        {
            HttpClient.AddRequestHeader(HttpHeaders.AcceptEncoding, "gzip, deflate, br");
        }

        protected void AddAcceptLanguageHeader()
        {
            HttpClient.AddRequestHeader(HttpHeaders.AcceptLanguage, "en-US,en;q=0.8,en-US;q=0.6,en;q=0.4");
        }

        protected void AddAuthorizationHeader(string authCode)
        {
            HttpClient.AddRequestHeader("Authorization", "Bearer " + authCode);
        }

        public void SetCookieContainer(CookieContainer cookieContainer)
        {
            HttpClient.MessageHandler.CookieContainer = cookieContainer;
        }

        protected static async Task<T> DeserializeAsync<T>(HttpResponseMessage message) where T : class
        {
            message.EnsureSuccessStatusCode();
            var messageContent = await message.Content.ReadAsStringAsync();
            T deserializedObject = null;

            try
            {
                deserializedObject = JsonConvert.DeserializeObject<T>(messageContent, JsonSerializerSettings);
            }
            catch (JsonSerializationException serializationException)
            {
                try
                {
                    var futError = JsonConvert.DeserializeObject<FutError>(messageContent, JsonSerializerSettings);
                    MapAndThrowException<FutError>(serializationException, futError);
                }
                catch (JsonSerializationException)
                {
                    throw serializationException;
                }
            }

            if (deserializedObject == null)
                throw new Exception($"Unable to deserialize {typeof(T).FullName}");
            return deserializedObject;
        }

        private static void MapAndThrowException<T>(Exception exception, FutError futError) where T : class
        {
            switch (futError.Code)
            {
                case FutErrorCode.ExpiredSession:
                    futError.Reason = $"Session expired - You need to relogin";
                    throw new ExpiredSessionException(futError, exception);

                case FutErrorCode.NotFound:
                    futError.Reason = $"Destination not found (404)";
                    throw new NotFoundException(futError, exception);

                case FutErrorCode.Conflict:
                    if (Activator.CreateInstance(typeof(T)) is ListAuctionResponse)
                    {
                        futError.Reason = $"Conflict - Please check the pricerange";
                        throw new WrongPriceRangeException(futError, exception);
                    }
                    else
                    {
                        futError.Reason = $"Conflict";
                        throw new ConflictException(futError, exception);
                    }

                case FutErrorCode.BadRequest:
                    futError.Reason = $"Bad Request";
                    throw new BadRequestException(futError, exception);

                case FutErrorCode.PermissionDenied:
                    futError.Reason = $"Permission Denied - You need to find the auction before bidding / Auction not existing";
                    throw new PermissionDeniedException(futError, exception);

                case FutErrorCode.NotEnoughCredit:
                    futError.Reason = $"Not enough coins";
                    throw new NotEnoughCreditException(futError, exception);

                case FutErrorCode.NoSuchTradeExists:
                    futError.Reason = $"Trade not found";
                    throw new NoSuchTradeExistsException(futError, exception);

                case FutErrorCode.InternalServerError:
                    if (Activator.CreateInstance(typeof(T)) is AuctionResponse)
                    {
                        futError.Reason = $"Temporary Transfermarket BAN detected";
                        throw new TemporaryBanException(futError, exception);
                    }
                    else
                    {
                        futError.Reason = $"Internal Server Error - FUT unavailable";
                        throw new InternalServerException(futError, exception);
                    }

                case FutErrorCode.ServiceUnavailable:
                    futError.Reason = $"Service Unavailable";
                    throw new ServiceUnavailableException(futError, exception);

                case FutErrorCode.InvalidDeck:
                    futError.Reason = $"Invalid Deck";
                    throw new InvalidDeckException(futError, exception);

                case FutErrorCode.DestinationFull:
                    futError.Reason = $"Destination Pile (Watchlist / Transferlist) is full";
                    throw new DestinationFullException(futError, exception);

                case FutErrorCode.BadGateway:
                    futError.Reason = $"Bad Gateway - Please try to relogin";
                    throw new BadGatewayException(futError, exception);

                case FutErrorCode.InvalidCookie:
                    futError.Reason = $"Invalid Cookie - Please try to relogin";
                    throw new InvalidCookieException(futError, exception);

                case FutErrorCode.InvalidTransaction:
                    futError.Reason = $"Invalid Transaction (i.e. if you try to list an aution which is still active)";
                    throw new InvalidTransactionException(futError, exception);

                case FutErrorCode.PurchasedItemsFull:
                    futError.Reason = $"Purchased Items Pile is full - You need to assign them to club or transferlist";
                    throw new PurchasedItemsFullException(futError, exception);

                case FutErrorCode.NoRemainingAuthenticationAttemptsAccountLocked:
                    futError.Reason = $"Account locked - You need to login in a validated console or pc";
                    throw new AccountLockedException(futError, exception);

                case FutErrorCode.TooManyRequests:
                    futError.Reason = $"Too many requests";
                    throw new TemporaryBanException(futError, exception);

                case FutErrorCode.UpgradeRequired:
                    futError.Reason = $"Auction state is stale";
                    throw new TemporaryBanException(futError, exception);

                case FutErrorCode.ServiceDisabled:
                    futError.Reason = $"FUT WebApp / CompanionApp not reachable";
                    throw new ServiceDisabledException(futError, exception);

                case FutErrorCode.TransfermarketBlocked:
                    futError.Reason = $"Transfermarket not available";
                    throw new TransfermarketBlockedException(futError, exception);

                case FutErrorCode.FunCaptchaTriggered:
                    futError.Reason = $"FunCaptcha has been triggered";
                    throw new FunCaptchaTriggeredException(futError, exception);

                case FutErrorCode.DuplicateItem:
                    futError.Reason = $"Item is a duplicate and cannot be sent to the club";
                    throw new DuplicateItemException(futError, exception);

                default:
                    var newException = new FutErrorException(futError, exception);
                    throw new FutException(string.Format("Unknown EA error, please report it on GitHub - {0}", newException.Message), newException);
            }
        }
    }
}
