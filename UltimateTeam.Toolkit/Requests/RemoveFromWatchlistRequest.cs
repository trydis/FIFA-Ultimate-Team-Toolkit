using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models.Auction;
using UltimateTeam.Toolkit.RequestFactory;

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
            var uriString = Resources.FutHome + Resources.Watchlist + $"?tradeId={tradeIds}";
            Task<HttpResponseMessage> removeFromWatchlistResponseMessageTask;

            AddCommonHeaders();
            removeFromWatchlistResponseMessageTask = HttpClient.DeleteAsync(uriString);

            var removeFromWatchlistResponseMessage = await removeFromWatchlistResponseMessageTask.ConfigureAwait(false);
            removeFromWatchlistResponseMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}