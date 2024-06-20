using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PurchasedItemsRequest : FutRequestBase, IFutRequest<PurchasedItemsResponse>
    {
        public async Task<PurchasedItemsResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.PurchasedItems;

            AddCommonHeaders();
            var purchasedItemsMessage = await HttpClient
                .GetAsync(uriString)
                .ConfigureAwait(false);

            return await DeserializeAsync<PurchasedItemsResponse>(purchasedItemsMessage);
        }
    }
}
