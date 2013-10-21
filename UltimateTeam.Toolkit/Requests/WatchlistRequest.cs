using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class WatchlistRequest : FutRequestBase, IFutRequest<WatchlistResponse>
    {
        public async Task<WatchlistResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var watchlistResponseMessage = await HttpClient
                .GetAsync(string.Format(Resources.FutHome + Resources.Watchlist))
                .ConfigureAwait(false);

            return await Deserialize<WatchlistResponse>(watchlistResponseMessage);
        }
    }
}
