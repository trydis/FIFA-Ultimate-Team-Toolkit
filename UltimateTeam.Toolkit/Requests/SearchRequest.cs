using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SearchRequest : FutRequestBase, IFutRequest<AuctionResponse>
    {
        private const byte PageSize = 12;

        private readonly SearchParameters _searchParameters;

        public static string PhishingToken { get; set; }

        public static string SessionId { get; set; }

        public SearchRequest(SearchParameters searchParameters)
        {
            _searchParameters = searchParameters;
        }

        public async Task<AuctionResponse> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.TransferMarket + "?start={0}&num={1}", (_searchParameters.Page - 1) * PageSize, PageSize + 1);
            _searchParameters.BuildUriString(ref uriString);
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(NonStandardHttpHeaders.MethodOverride, "GET");
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(NonStandardHttpHeaders.PhishingToken, PhishingToken);
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(NonStandardHttpHeaders.EmbedError, "true");
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(NonStandardHttpHeaders.SessionId, SessionId);
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaders.AcceptEncoding, "gzip,deflate,sdch");
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaders.AcceptLanguage, "en-US,en;q=0.8");
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaders.Accept, "application/json");
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaders.ContentType, "application/json");
            HttpClient.DefaultRequestHeaders.Referrer = new Uri("http://www.easports.com/iframe/fut/bundles/futweb/web/flash/FifaUltimateTeam.swf");
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaders.UserAgent, "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.62 Safari/537.36");
            var searchResponseMessage = await HttpClient.PostAsync(uriString, new StringContent(" "));
            searchResponseMessage.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<AuctionResponse>(await searchResponseMessage.Content.ReadAsStringAsync());
        }
    }
}