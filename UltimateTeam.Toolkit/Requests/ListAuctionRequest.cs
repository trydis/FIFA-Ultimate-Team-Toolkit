using System;
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
            var uriString = string.Format(Resources.FutHome + Resources.Auctionhouse);
            var content = $"{{\"buyNowPrice\":{_auctionDetails.BuyNowPrice},\"startingBid\":{_auctionDetails.StartingBid}," +
                          $"\"duration\":{(uint)_auctionDetails.AuctionDuration},\"itemData\":{{\"id\":{_auctionDetails.ItemDataId}}}}}";

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Post);
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"?_={DateTime.Now.ToUnixTime()}";
            }

            var tradepileResponseMessage = await HttpClient
                .PostAsync(uriString, new StringContent(content))
                .ConfigureAwait(false);

            return await DeserializeAsync<ListAuctionResponse>(tradepileResponseMessage);
        }
    }
}
