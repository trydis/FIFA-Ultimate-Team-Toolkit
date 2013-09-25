using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class RemoveFromWatchlistRequest : FutRequestBase, IFutRequest<byte>
    {
        private readonly AuctionInfo _auctioninfo;

        public RemoveFromWatchlistRequest(AuctionInfo auctioninfo)
        {
            auctioninfo.ThrowIfNullArgument();
            _auctioninfo = auctioninfo;
        }

        public async Task<byte> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.Watchlist + "?tradeId={0}", _auctioninfo.TradeId);
            AddMethodOverrideHeader(HttpMethod.Delete);
            AddCommonHeaders();
            var removeFromWatchlistResponseMessage = await HttpClient.PostAsync(uriString, new StringContent(" "));
            removeFromWatchlistResponseMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}