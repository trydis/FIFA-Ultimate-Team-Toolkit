using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;
using System;

namespace UltimateTeam.Toolkit.Requests
{
    internal class TradeStatusRequest : FutRequestBase, IFutRequest<AuctionResponse>
    {
        private readonly IEnumerable<long> _tradeIds;
        private AppVersion _appVersion;

        public TradeStatusRequest(IEnumerable<long> tradeIds)
        {
            tradeIds.ThrowIfNullArgument();
            _tradeIds = tradeIds;
        }

        public async Task<AuctionResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddCommonHeaders();
                AddMethodOverrideHeader(HttpMethod.Get);
                var tradeStatusResponseMessage = await HttpClient
                    .PostAsync(
                    string.Format(Resources.FutHome + Resources.TradeStatus, string.Join("%2C", _tradeIds)),
                    new StringContent(" "))
                    .ConfigureAwait(false);

                return await Deserialize<AuctionResponse>(tradeStatusResponseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                var tradeStatusResponseMessage = await HttpClient
                    .GetAsync(
                    string.Format(Resources.FutHome + Resources.TradeStatus + "&_=" + DateTimeExtensions.ToUnixTime(DateTime.Now), string.Join("%2C", _tradeIds)))
                    .ConfigureAwait(false);

                return await Deserialize<AuctionResponse>(tradeStatusResponseMessage);
            }
            else
            {
                return null;
            }
        }
    }
}
