using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SearchRequest : FutRequestBase, IFutRequest<AuctionResponse>
    {
        private readonly SearchParameters _searchParameters;

        public SearchRequest(SearchParameters searchParameters)
        {
            searchParameters.ThrowIfNullArgument();
            _searchParameters = searchParameters;
        }

        public async Task<AuctionResponse> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.TransferMarket + "?start={0}&num={1}",
                                          (_searchParameters.Page - 1) * _searchParameters.PageSize, _searchParameters.PageSize + 1);
            _searchParameters.BuildUriString(ref uriString);
            Task<HttpResponseMessage> searchResponseMessageTask;

            AddCommonHeaders();
            searchResponseMessageTask = HttpClient.GetAsync(uriString);

            var searchResponseMessage = await searchResponseMessageTask.ConfigureAwait(false);

            return await DeserializeAsync<AuctionResponse>(searchResponseMessage);
        }
    }
}