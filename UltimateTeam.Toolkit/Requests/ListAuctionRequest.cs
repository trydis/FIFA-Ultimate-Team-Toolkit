using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ListAuctionRequest : FutRequestBase, IFutRequest<ListAuctionResponse>
    {
        private readonly AuctionDetails _auctionDetails;

        public ListAuctionRequest(AuctionDetails auctionDetails)
        {
            auctionDetails.ThrowIfNullArgument();
            _auctionDetails = auctionDetails;
        }

        public async Task<ListAuctionResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Post);
            AddCommonHeaders();
            var content = string.Format("{{\"buyNowPrice\":{0},\"startingBid\":{1},\"duration\":{2},\"itemData\":{{\"id\":{3}}}}}",
                _auctionDetails.BuyNowPrice, _auctionDetails.StartingBid, (uint)_auctionDetails.AuctionDuration, _auctionDetails.ItemDataId);
            var tradepileResponseMessage = await HttpClient
                .PostAsync(string.Format(Resources.FutHome + Resources.Auctionhouse), new StringContent(content))
                .ConfigureAwait(false);

            return await Deserialize<ListAuctionResponse>(tradepileResponseMessage);
        }
    }
}
