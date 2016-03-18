using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;
using System.Collections.Generic;
using System.Linq;
using System;
using UltimateTeam.Toolkit.Exceptions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class RemoveFromWatchlistRequest : FutRequestBase, IFutRequest<byte>
    {
        private readonly IEnumerable<AuctionInfo> _auctioninfo;
        private AppVersion _appVersion;

        public RemoveFromWatchlistRequest(IEnumerable<AuctionInfo> auctioninfo)
        {
            auctioninfo.ThrowIfNullArgument();
            _auctioninfo = auctioninfo;
        }

        public async Task<byte> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddMethodOverrideHeader(HttpMethod.Delete);
                AddCommonHeaders();
                var tradeIds = string.Join("%2C", _auctioninfo.Select(p => p.TradeId));
                var uriString = string.Format(Resources.FutHome + Resources.Watchlist + "?tradeId={0}", tradeIds);

                var removeFromWatchlistResponseMessage = await HttpClient
                    .PostAsync(uriString, new StringContent(" "))
                    .ConfigureAwait(false);
                removeFromWatchlistResponseMessage.EnsureSuccessStatusCode();

                return 0;
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                var tradeIds = string.Join("%2C", _auctioninfo.Select(p => p.TradeId));
                var uriString = string.Format(Resources.FutHome + Resources.Watchlist + "?tradeId={0}" + "&_=" + DateTimeExtensions.ToUnixTime(DateTime.Now), tradeIds);

                var removeFromWatchlistResponseMessage = await HttpClient
                    .DeleteAsync(uriString)
                    .ConfigureAwait(false);
                removeFromWatchlistResponseMessage.EnsureSuccessStatusCode();

                return 0;
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", appVersion.ToString()));
            }
        }
    }
}