using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class AddToWatchlistRequest : FutRequestBase, IFutRequest<byte>
    {
        private readonly IEnumerable<AuctionInfo> _auctioninfo;

        public AddToWatchlistRequest(IEnumerable<AuctionInfo> auctioninfo)
        {
            auctioninfo.ThrowIfNullArgument();
            _auctioninfo = auctioninfo;
        }

        public async Task<byte> PerformRequestAsync()
        {
            var tradeIds = string.Join("%2C", _auctioninfo.Select(p => p.TradeId));
            var content = $"{{\"auctionInfo\":[{{\"id\":{tradeIds}}}]}}";
            var uriString = Resources.FutHome + Resources.Watchlist + $"?tradeId={tradeIds}";
            ConfiguredTaskAwaitable<HttpResponseMessage> addToWatchlistTask;

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Put);
                addToWatchlistTask = HttpClient
                    .PostAsync(uriString, new StringContent(content))
                    .ConfigureAwait(false);

            }
            if (AppVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                uriString += $"&_={DateTime.Now.ToUnixTime()}";

                addToWatchlistTask = HttpClient
                    .PutAsync(uriString, new StringContent(content))
                    .ConfigureAwait(false);
            }

            var addToWatchlistResponseMessage = await addToWatchlistTask;
            addToWatchlistResponseMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}
