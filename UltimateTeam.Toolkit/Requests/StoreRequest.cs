using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class StoreRequest : FutRequestBase, IFutRequest<StoreResponse>
    {
        public async Task<StoreResponse> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.Store);

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Get);
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"?_={DateTime.Now.ToUnixTime()}";
            }

            var storeResponseMessage = await HttpClient
                .PostAsync(uriString, new StringContent(" "))
                .ConfigureAwait(false);

            return await DeserializeAsync<StoreResponse>(storeResponseMessage);
        }
    }
}