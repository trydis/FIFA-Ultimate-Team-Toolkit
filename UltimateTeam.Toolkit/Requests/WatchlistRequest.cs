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
        private AppVersion _appVersion;

        public async Task<WatchlistResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddMethodOverrideHeader(HttpMethod.Get);
                AddCommonHeaders();
                var watchlistResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.Watchlist))
                    .ConfigureAwait(false);

                return await Deserialize<WatchlistResponse>(watchlistResponseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                var watchlistResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.Watchlist + "?_=" + DateTimeExtensions.ToUnixTime(DateTime.Now)))
                    .ConfigureAwait(false);

                return await Deserialize<WatchlistResponse>(watchlistResponseMessage);
            }
            else
            {
                return null;
            }
        }
    }
}
