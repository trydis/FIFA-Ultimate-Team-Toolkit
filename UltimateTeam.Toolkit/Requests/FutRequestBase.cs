using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.PhishingToken, _phishingToken);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.EmbedError, "true");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, _sessionId);
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
            AddAcceptHeader("application/json");
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, "application/json");
            AddReferrerHeader("http://www.easports.com/iframe/fut/bundles/futweb/web/flash/FifaUltimateTeam.swf");
            AddUserAgent();
            HttpClient.AddConnectionKeepAliveHeader();
        }

        protected void AddUserAgent()
        {
            HttpClient.AddRequestHeader(HttpHeaders.UserAgent, "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.62 Safari/537.36");
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
            HttpClient.AddRequestHeader(HttpHeaders.AcceptEncoding, "gzip,deflate,sdch");
        }

        protected void AddAcceptLanguageHeader()
        {
            HttpClient.AddRequestHeader(HttpHeaders.AcceptLanguage, "en-US,en;q=0.8");
        }

        protected void AddMethodOverrideHeader(HttpMethod httpMethod)
        {
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.MethodOverride, httpMethod.Method);
        }

        protected static async Task<T> Deserialize<T>(HttpResponseMessage message) where T : class
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
                default:
                    var newException = new FutErrorException(futError, exception);
                    throw new FutException(string.Format("Unknown EA error, please report on GitHub - {0}", newException.Message), newException);
            }
        }
    }
}
