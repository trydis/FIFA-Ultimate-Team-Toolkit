using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ConsumablesRequest : FutRequestBase, IFutRequest<ConsumablesResponse>
    {
        public async Task<ConsumablesResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.Consumables;

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Get);
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"?_={DateTime.Now.ToUnixTime()}";
            }

            var consumablesResponseMessage = await HttpClient
                .GetAsync(string.Format(uriString))
                .ConfigureAwait(false);

            return await DeserializeAsync<ConsumablesResponse>(consumablesResponseMessage);
        }
    }
}
