using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    public abstract class FutRequestBase
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error };

        private string _phishingToken;

        private string _sessionId;

        private string _nucleusId;

        private IHttpClient _httpClient;

        public string PhishingToken
        {
            set
            {
                value.ThrowIfInvalidArgument();
                _phishingToken = value;
            }
        }

        public string SessionId
        {
            set
            {
                value.ThrowIfInvalidArgument();
                _sessionId = value;
            }
        }

        public string NucleusId
        {
            set
            {
                value.ThrowIfInvalidArgument();
                _nucleusId = value;
            }
        }

        public AppVersion AppVersion { get; set; }

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

        protected void AddCommonHeaders(HttpMethod methodOverrideHeader = null)
        {
            HttpClient.ClearRequestHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.PhishingToken, _phishingToken);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.EmbedError, "true");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, _sessionId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.RequestedWith, "ShockwaveFlash/21.0.0.182");
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
            AddAcceptHeader("application/json");
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, "application/json");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.Origin, @"https://www.easports.com");
            AddReferrerHeader("http://www.easports.com/iframe/fut/bundles/futweb/web/flash/FifaUltimateTeam.swf?cl=155438");
            AddUserAgent();
            HttpClient.AddConnectionKeepAliveHeader();
            if (methodOverrideHeader != null)
            {
                AddMethodOverrideHeader(methodOverrideHeader);
            }
        }

        protected void AddCaptchaHeaders()
        {
            HttpClient.ClearRequestHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusId, _nucleusId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.EmbedError, "true");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.Route, "https://utas.s2.fut.ea.com");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.RequestedWith, "XMLHttpRequest");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, _sessionId);
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
            AddAcceptHeader("application/json");
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, "application/json");
            AddReferrerHeader("http://www.easports.com/iframe/fut17/?baseShowoffUrl=https%3A%2F%2Fwww.easports.com%2Fuk%2Ffifa%2Fultimate-team%2Fweb-app%2Fshow-off&guest_app_uri=http%3A%2F%2Fwww.easports.com%2Fuk%2Ffifa%2Fultimate-team%2Fweb-app&locale=en_GB");
            AddUserAgent();
        }

        protected void AddCommonMobileHeaders()
        {
            HttpClient.ClearRequestHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.PhishingToken, _phishingToken);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusId, _nucleusId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, _sessionId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.CSP, "active");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.Origin, @"file://");
            AddAcceptEncodingHeader();
            AddAcceptMobileLanguageHeader();
            AddAcceptHeader("text/plain, */*; q=0.01");
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, "application/json");
            AddMobileUserAgent();
            HttpClient.AddConnectionKeepAliveHeader();
        }

        protected void AddMobileLoginHeaders()
        {
            HttpClient.ClearRequestHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.CSP, "active");
            AddAcceptHeader("*/*");
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, "application/json");
            AddAcceptEncodingHeader();
            AddAcceptMobileLanguageHeader();
            AddMobileUserAgent();
        }

        protected void AddMobileCaptchaHeaders()
        {
            HttpClient.ClearRequestHeaders();
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusId, _nucleusId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.PhishingToken, _phishingToken);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, _sessionId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.CSP, "active");
            AddAcceptHeader("*/*");
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, "application/json");
            AddAcceptEncodingHeader();
            AddAcceptMobileLanguageHeader();
            AddMobileUserAgent();
        }

        protected void AddPinHeaders()
        {
            HttpClient.ClearRequestHeaders();
            HttpClient.AddConnectionKeepAliveHeader();
            HttpClient.AddRequestHeader("Origin", "https://www.easports.com");
            HttpClient.AddRequestHeader("x-ea-taxv", "1.1");
            HttpClient.AddRequestHeader("x-ea-game-type", "sku");
            AddUserAgent();
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, "application/json");
            HttpClient.AddRequestHeader("X-Requested-With", "ShockwaveFlash/20.0.0.286");
            HttpClient.AddRequestHeader("x-ea-game-id", "fifa17");
            AddAcceptHeader("*/*");
            AddReferrerHeader("https://www.easports.com/iframe/fut17/bundles/futweb/web/flash/FifaUltimateTeam.swf?cl=155438");
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
        }

        protected void AddPinHeadersMobile()
        {
            HttpClient.ClearRequestHeaders();
            HttpClient.AddConnectionKeepAliveHeader();
            HttpClient.AddRequestHeader("Origin", "file://");
            HttpClient.AddRequestHeader("x-ea-taxv", "1");
            HttpClient.AddRequestHeader("CSP", "active");
            AddMobileUserAgent();
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, "application/json");
            AddAcceptHeader("text/plain, */*; q=0.01");
            HttpClient.AddRequestHeader("x-ea-game-id-type", "sellid");
            HttpClient.AddRequestHeader("x-ea-game-id", "859051");
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
        }

        protected void AddAnonymousHeader()
        {
            AddUserAgent();
            AddAcceptHeader("*/*");
            AddReferrerHeader(Resources.BaseShowoff);
            HttpClient.AddRequestHeader("X-Requested-With", "ShockwaveFlash/21.0.0.182");
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
            HttpClient.AddConnectionKeepAliveHeader();
        }

        protected void AddAnonymousMobileHeader()
        {
            AddMobileUserAgent();
            AddAcceptHeader("*/*");
            HttpClient.AddRequestHeader("CSP", "active");
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
            HttpClient.AddConnectionKeepAliveHeader();
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
            HttpClient.AddRequestHeader(HttpHeaders.UserAgent, "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.62 Safari/537.36");
        }

        protected void AddMobileUserAgent()
        {
            HttpClient.AddRequestHeader(HttpHeaders.UserAgent, "User-Agent: Mozilla/5.0 (Linux; Android 4.2.2; AndyWin Build/JDQ39E) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.130 Crosswalk/14.43.343.25 Safari/537.36");
        }

        protected void AddAcceptHeader(string value)
        {
            HttpClient.AddRequestHeader(HttpHeaders.Accept, value);
        }

        protected void AddReferrerHeader(string value)
        {
            HttpClient.SetReferrerUri(value);
        }

        protected void AddAcceptEncodingHeader()
        {
            HttpClient.AddRequestHeader(HttpHeaders.AcceptEncoding, "gzip,deflate");
        }

        protected void AddAcceptLanguageHeader()
        {
            HttpClient.AddRequestHeader(HttpHeaders.AcceptLanguage, "en-US,en;q=0.8,en-US;q=0.6,en;q=0.4");
        }

        protected void AddAcceptMobileLanguageHeader()
        {
            HttpClient.AddRequestHeader(HttpHeaders.AcceptLanguage, "en-US,en");
        }

        protected void AddMethodOverrideHeader(HttpMethod httpMethod)
        {
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.MethodOverride, httpMethod.Method);
        }

        protected void AddAuthorizationHeader(string authCode)
        {
            HttpClient.AddRequestHeader("Authorization", "Bearer " + authCode);
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
                    MapAndThrowException(serializationException, futError);
                }
                catch (JsonSerializationException)
                {
                    throw serializationException;
                }
            }

            return deserializedObject;
        }

        private static void MapAndThrowException(Exception exception, FutError futError)
        {
            // TODO: Should extract this to a separate class and keep them in a Dictionary<FutErrorCode, Func<FutError, Exception, FutErrorException>>

            switch (futError.Code)
            {
                case FutErrorCode.ExpiredSession:
                    throw new ExpiredSessionException(futError, exception);
                case FutErrorCode.NotFound:
                    throw new NotFoundException(futError, exception);
                case FutErrorCode.Conflict:
                    throw new ConflictException(futError, exception);
                case FutErrorCode.BadRequest:
                    throw new BadRequestException(futError, exception);
                case FutErrorCode.PermissionDenied:
                    throw new PermissionDeniedException(futError, exception);
                case FutErrorCode.NotEnoughCredit:
                    throw new NotEnoughCreditException(futError, exception);
                case FutErrorCode.NoSuchTradeExists:
                    throw new NoSuchTradeExistsException(futError, exception);
                case FutErrorCode.InternalServerError:
                    throw new InternalServerException(futError, exception);
                case FutErrorCode.ServiceUnavailable:
                    throw new ServiceUnavailableException(futError, exception);
                case FutErrorCode.InvalidDeck:
                    throw new InvalidDeckException(futError, exception);
                case FutErrorCode.DestinationFull:
                    throw new DestinationFullException(futError, exception);
                case FutErrorCode.CaptchaTriggered:
                    throw new CaptchaTriggeredException(futError, exception);
                case FutErrorCode.PurchasedItemsFull:
                    throw new PurchasedItemsFullException(futError, exception);
                default:
                    var newException = new FutErrorException(futError, exception);
                    throw new FutException(string.Format("Unknown EA error, please report on GitHub - {0}", newException.Message), newException);
            }
        }
    }
}
