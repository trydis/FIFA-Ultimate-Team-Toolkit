using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    public abstract class FutRequestBase
    {
        private string _phishingToken;

        private string _sessionId;

        private readonly HttpClientHandler _messageHandler;

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

        public HttpClientHandler MessageHandler
        {
            get { return _messageHandler; }
        }

        protected readonly HttpClient HttpClient;

        protected FutRequestBase()
        {
            _messageHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip
            };
            HttpClient = new HttpClient(MessageHandler);
            HttpClient.DefaultRequestHeaders.ExpectContinue = false;
        }

        protected void AddCommonHeaders()
        {
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(NonStandardHttpHeaders.PhishingToken, _phishingToken);
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(NonStandardHttpHeaders.EmbedError, "true");
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(NonStandardHttpHeaders.SessionId, _sessionId);
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
            AddAcceptHeader("application/json");
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaders.ContentType, "application/json");
            AddReferrerHeader("http://www.easports.com/iframe/fut/bundles/futweb/web/flash/FifaUltimateTeam.swf");
            AddUserAgent();
            HttpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
        }

        protected void AddUserAgent()
        {
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaders.UserAgent, "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.62 Safari/537.36");
        }

        protected void AddAcceptHeader(string value)
        {
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaders.Accept, value);            
        }

        protected void AddReferrerHeader(string value)
        {
            HttpClient.DefaultRequestHeaders.Referrer = new Uri(value);
        }

        protected void AddAcceptEncodingHeader()
        {
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaders.AcceptEncoding, "gzip,deflate,sdch");            
        }

        protected void AddAcceptLanguageHeader()
        {
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaders.AcceptLanguage, "en-US,en;q=0.8");            
        }

        protected void AddMethodOverrideHeader(HttpMethod httpMethod)
        {
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(NonStandardHttpHeaders.MethodOverride, httpMethod.Method);                        
        }

        protected static async Task<T> Deserialize<T>(HttpResponseMessage message)
        {
            message.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await message.Content.ReadAsStringAsync());
        }
    }
}