using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class WatchlistRequest : FutRequestBase, IFutRequest<WatchlistResponse>
    {
        public async Task<WatchlistResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.Watchlist;
            Task<HttpResponseMessage> watchlistResponseMessageTask;

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Get);
                watchlistResponseMessageTask = HttpClient.GetAsync(uriString);
            }
            else
            {
                AddCommonMobileHeaders();
                watchlistResponseMessageTask = HttpClient.GetAsync(uriString + $"?_={ DateTime.Now.ToUnixTime()}");
            }

            var watchlistResponseMessage = await watchlistResponseMessageTask.ConfigureAwait(false);

            return await DeserializeAsync<WatchlistResponse>(watchlistResponseMessage);
        }
    }
}
