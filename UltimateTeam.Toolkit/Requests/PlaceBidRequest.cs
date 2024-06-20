using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Models.Auction;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PlaceBidRequest : FutRequestBase, IFutRequest<AuctionResponse>
    {
        private readonly AuctionInfo _auctionInfo;
        private readonly uint _bidAmount;

        public PlaceBidRequest(AuctionInfo auctionInfo, uint bidAmount)
        {
            auctionInfo.ThrowIfNullArgument();
            _auctionInfo = auctionInfo;
            _bidAmount = bidAmount;
        }

        public async Task<AuctionResponse> PerformRequestAsync()
        {
            Task<HttpResponseMessage> bidResponseMessageTask;
            var uriString = string.Format(Resources.FutHome + Resources.Bid, _auctionInfo.TradeId);
            var content = new StringContent($"{{\"bid\":{_bidAmount}}}");

            AddCommonHeaders();
            bidResponseMessageTask = HttpClient.PutAsync(uriString, content);

            var bidResponseMessage = await bidResponseMessageTask.ConfigureAwait(false);

            return await DeserializeAsync<AuctionResponse>(bidResponseMessage);
        }
    }
}