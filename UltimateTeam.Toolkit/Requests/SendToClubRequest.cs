using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Models.Auction;
using UltimateTeam.Toolkit.Models.Player;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SendToClubRequest : FutRequestBase, IFutRequest<SendToClubResponse>
    {
        private readonly ItemData? _itemData;
        private readonly IEnumerable<long>? _itemIds;
        private readonly AuctionInfo? _auctionInfo;

        public SendToClubRequest(IEnumerable<long> itemIds)
        {
            itemIds.ThrowIfNullArgument();
            _itemIds = itemIds;
        }

        public SendToClubRequest(ItemData itemData, AuctionInfo auctionInfo)
        {
            itemData.ThrowIfNullArgument();
            _itemData = itemData;
            _auctionInfo = auctionInfo;
        }

        public async Task<SendToClubResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.Item;
            Task<HttpResponseMessage> clubResponseMessageTask;

            //Apply Draft-Token & Credits to club
            HttpContent? content = null;
            if (_auctionInfo == null && _itemIds.Count() > 0)
            {
                string contentIds = string.Join(",", _itemIds.Select(id => $"{{\"id\":{id},\"pile\":\"club\"}}"));
                content = new StringContent($"{{\"itemData\":[{contentIds}]}}");
            }
            else if (_itemData != null && _auctionInfo != null)
            {
                content = _itemData.CardSubTypeId == 231
                                  ? new StringContent("{\"apply\":[]}")
                                  : new StringContent($"{{\"itemData\":[{{\"id\":{_itemData.Id},\"pile\":\"club\",\"tradeId\":\"{_auctionInfo.TradeId}\"}}]}}");
            }
            else { throw new ArgumentException(); }

            AddCommonHeaders();
            clubResponseMessageTask = HttpClient.PutAsync(uriString, content);
            var clubResponseMessage = await clubResponseMessageTask.ConfigureAwait(false);

            return await DeserializeAsync<SendToClubResponse>(clubResponseMessage);
        }
    }
}