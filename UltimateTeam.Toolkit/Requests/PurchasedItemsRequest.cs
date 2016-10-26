using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PurchasedItemsRequest : FutRequestBase, IFutRequest<PurchasedItemsResponse>
    {
        public async Task<PurchasedItemsResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.PurchasedItems;

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Get);
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"?_={DateTime.Now.ToUnixTime()}";
            }

            var purchasedItemsMessage = await HttpClient
                .GetAsync(uriString)
                .ConfigureAwait(false);

            return await DeserializeAsync<PurchasedItemsResponse>(purchasedItemsMessage);
        }
    }
}
