using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Exceptions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class WatchlistCleanupRequest : FutRequestBase
    {
        private readonly AuctionInfo _auctioninfo;

        public WatchlistCleanupRequest(AuctionInfo auctioninfo)
        {
            auctioninfo.ThrowIfNullArgument();
            _auctioninfo = auctioninfo;
        }
        
        public async Task PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.Watchlist + "?tradeId={0}", _auctioninfo.TradeId);
            AddMethodOverrideHeader(HttpMethod.Delete);
            AddCommonHeaders();
            try
            {
                var watchlistCleanupResponseMessage = await HttpClient.PostAsync(uriString, new StringContent(" "));
                watchlistCleanupResponseMessage.EnsureSuccessStatusCode();
            }

            catch (System.Exception e)
            {
                throw new FutException("Failed to clean up Watchlist", e.InnerException);

            }
        }
    }
}