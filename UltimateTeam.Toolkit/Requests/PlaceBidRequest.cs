using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

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
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(NonStandardHttpHeaders.MethodOverride, "PUT");
            AddCommonHeaders();
            var content = string.Format("{{\"bid\":{0}}}", _bidAmount);
            var bidResponseMessage = await HttpClient.PostAsync(string.Format(Resources.FutHome + Resources.Bid, _auctionInfo.TradeId), new StringContent(content));
            bidResponseMessage.EnsureSuccessStatusCode();
            var auctionResponse = JsonConvert.DeserializeObject<AuctionResponse>(await bidResponseMessage.Content.ReadAsStringAsync());

            return auctionResponse;
        }
    }
}