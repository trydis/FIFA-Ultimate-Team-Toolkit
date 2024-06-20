using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Models.Auction;
using UltimateTeam.Toolkit.Models.Player;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SendToTradePileRequest : FutRequestBase, IFutRequest<SendToTradePileResponse>
    {
        private readonly ItemData? _itemData;
        private readonly IEnumerable<long>? _itemIds;
        private readonly AuctionInfo? _auctionInfo;

        public SendToTradePileRequest(IEnumerable<long> itemIds)
        {
            itemIds.ThrowIfNullArgument();
            _itemIds = itemIds;
        }

        public SendToTradePileRequest(ItemData itemData, AuctionInfo auctionInfo)
        {
            itemData.ThrowIfNullArgument();
            _itemData = itemData;
            _auctionInfo = auctionInfo;
        }

        public async Task<SendToTradePileResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.Item;
            Task<HttpResponseMessage> tradepileResponseMessageTask;

            HttpContent? content = null;
            if (_auctionInfo == null && _itemIds.Count() > 0)
            {
                string contentIds = string.Join(",", _itemIds.Select(id => $"{{\"id\":{id},\"pile\":\"trade\"}}"));
                content = new StringContent($"{{\"itemData\":[{contentIds}]}}");
            }
            else if (_itemData != null && _auctionInfo != null)
            {
                if (_itemData.CardSubTypeId == 231)
                {
                    throw new FutException("CardSubTypeId 231 (Draft-Token or Credits) cannot be send to tradepile");
                }

                content = _itemData.CardSubTypeId == 231
                                  ? new StringContent("{\"apply\":[]}")
                                  : new StringContent($"{{\"itemData\":[{{\"id\":{_itemData.Id},\"pile\":\"trade\",\"tradeId\":\"{_auctionInfo.TradeId}\"}}]}}");
            }
            else { throw new ArgumentException(); }


            AddCommonHeaders();
            tradepileResponseMessageTask = HttpClient.PutAsync(uriString, content);
            var tradepileResponseMessage = await tradepileResponseMessageTask.ConfigureAwait(false);

            return await DeserializeAsync<SendToTradePileResponse>(tradepileResponseMessage);
        }
    }
}
