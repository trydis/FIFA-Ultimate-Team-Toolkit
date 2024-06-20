using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class QuickSellRequest : FutRequestBase, IFutRequest<QuickSellResponse>
    {
        private readonly IEnumerable<long> _itemIds;

        public QuickSellRequest(IEnumerable<long> itemIds)
        {
            _itemIds = itemIds;
        }

        public async Task<QuickSellResponse> PerformRequestAsync()
        {
            Task<HttpResponseMessage> quickSellResponseTask;
            AddCommonHeaders();

            if (_itemIds.Count() == 1)
            {
                var uriString = Resources.FutHomeDelete + string.Format(Resources.ItemDetails, _itemIds.First());
                quickSellResponseTask = HttpClient.DeleteAsync(uriString);
            }
            else
            {
                var uriString = Resources.FutHomeDelete + Resources.Item;
                var content = $"{{\"itemId\":[{string.Join(",", _itemIds)}]}}";
                quickSellResponseTask = HttpClient.PostAsync(uriString, new StringContent(content));
            }

            var quickSellResponseMessage = await quickSellResponseTask.ConfigureAwait(false);
            return await DeserializeAsync<QuickSellResponse>(quickSellResponseMessage).ConfigureAwait(false);
        }
    }
}
