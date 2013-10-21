using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PurchasedItemsRequest : FutRequestBase, IFutRequest<PurchasedItemsResponse>
    {
        public async Task<PurchasedItemsResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var purchasedItemsMessage = await HttpClient
                .GetAsync(string.Format(Resources.FutHome + Resources.PurchasedItems))
                .ConfigureAwait(false);

            return await Deserialize<PurchasedItemsResponse>(purchasedItemsMessage);
        }
    }
}
