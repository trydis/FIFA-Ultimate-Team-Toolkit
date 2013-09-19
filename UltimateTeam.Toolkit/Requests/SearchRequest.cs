using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SearchRequest : FutRequestBase, IFutRequest<AuctionResponse>
    {
        private const byte PageSize = 12;

        private readonly SearchParameters _searchParameters;

        public SearchRequest(SearchParameters searchParameters)
        {
            searchParameters.ThrowIfNullArgument();
            _searchParameters = searchParameters;
        }

        public async Task<AuctionResponse> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.TransferMarket + "?start={0}&num={1}", (_searchParameters.Page - 1) * PageSize, PageSize + 1);
            _searchParameters.BuildUriString(ref uriString);
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(NonStandardHttpHeaders.MethodOverride, "GET");
            AddCommonHeaders();
            var searchResponseMessage = await HttpClient.PostAsync(uriString, new StringContent(" "));
            searchResponseMessage.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<AuctionResponse>(await searchResponseMessage.Content.ReadAsStringAsync());
        }
    }
}