using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;
using System;

namespace UltimateTeam.Toolkit.Requests
{
    internal class TradePileRequest : FutRequestBase, IFutRequest<AuctionResponse>
    {
        private AppVersion _appVersion;

        public async Task<AuctionResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddCommonHeaders();
                AddMethodOverrideHeader(HttpMethod.Get);
                var tradePileResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.TradePile))
                    .ConfigureAwait(false);

                return await Deserialize<AuctionResponse>(tradePileResponseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                var tradePileResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.TradePile + "?_=" + DateTimeExtensions.ToUnixTime(DateTime.Now)))
                    .ConfigureAwait(false);

                return await Deserialize<AuctionResponse>(tradePileResponseMessage);
            }
            else
            {
                return null;
            }
        }
    }
}