using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace UltimateTeam.Toolkit.Requests
{
    internal class RemoveFromWatchlistRequest : FutRequestBase, IFutRequest<byte>
    {
        private readonly IEnumerable<AuctionInfo> _auctioninfo;

        public RemoveFromWatchlistRequest(IEnumerable<AuctionInfo> auctioninfo)
        {
            auctioninfo.ThrowIfNullArgument();
            _auctioninfo = auctioninfo;
        }

        public async Task<byte> PerformRequestAsync()
        {
            var tradeIds = string.Join("%2C", _auctioninfo.Select(p => p.TradeId));
            var uriString = string.Format(Resources.FutHome + Resources.Watchlist + "?tradeId={0}", tradeIds);
            AddMethodOverrideHeader(HttpMethod.Delete);
            AddCommonHeaders();
            var removeFromWatchlistResponseMessage = await HttpClient
                .PostAsync(uriString, new StringContent(" "))
                .ConfigureAwait(false);
            removeFromWatchlistResponseMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}