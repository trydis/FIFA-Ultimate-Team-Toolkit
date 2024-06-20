using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class WatchlistRequest : FutRequestBase, IFutRequest<WatchlistResponse>
    {
        public async Task<WatchlistResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.Watchlist;
            Task<HttpResponseMessage> watchlistResponseMessageTask;

            AddCommonHeaders();
            watchlistResponseMessageTask = HttpClient.GetAsync(uriString);
            var watchlistResponseMessage = await watchlistResponseMessageTask.ConfigureAwait(false);

            return await DeserializeAsync<WatchlistResponse>(watchlistResponseMessage);
        }
    }
}
