using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PurchasedItemsRequest : FutRequestBase, IFutRequest<PurchasedItemsResponse>
    {
        private AppVersion _appVersion;

        public async Task<PurchasedItemsResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddMethodOverrideHeader(HttpMethod.Get);
                AddCommonHeaders();
                var purchasedItemsMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.PurchasedItems))
                    .ConfigureAwait(false);

                return await Deserialize<PurchasedItemsResponse>(purchasedItemsMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                var purchasedItemsMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.PurchasedItems + "?_=" + DateTimeExtensions.ToUnixTime(DateTime.Now)))
                    .ConfigureAwait(false);

                return await Deserialize<PurchasedItemsResponse>(purchasedItemsMessage);
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", appVersion.ToString()));
            }
        }
    }
}
