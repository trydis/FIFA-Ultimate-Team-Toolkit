using System.Runtime.CompilerServices;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models.Auction;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class AddToWatchlistRequest : FutRequestBase, IFutRequest<byte>
    {
        private readonly AuctionInfo _auctioninfo;

        public AddToWatchlistRequest(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();
            _auctioninfo = auctionInfo;
        }

        public async Task<byte> PerformRequestAsync()
        {
            var content = $"{{\"auctionInfo\":[{{\"id\":{_auctioninfo.TradeId}}}]}}";
            var uriString = Resources.FutHome + Resources.Watchlist;
            ConfiguredTaskAwaitable<HttpResponseMessage> addToWatchlistTask;

            AddCommonHeaders();
            addToWatchlistTask = HttpClient
                .PutAsync(uriString, new StringContent(content))
                .ConfigureAwait(false);

            var addToWatchlistResponseMessage = await addToWatchlistTask;
            addToWatchlistResponseMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}
