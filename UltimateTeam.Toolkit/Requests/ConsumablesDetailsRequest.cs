using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ConsumablesDetailsRequest : FutRequestBase, IFutRequest<ConsumablesDetailsResponse>
    {
        public async Task<ConsumablesDetailsResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.ConsumablesDetails;

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Get);
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"?_={DateTime.Now.ToUnixTime()}";
            }

            var consumablesDetailsResponseMessage = await HttpClient
                .GetAsync(string.Format(uriString))
                .ConfigureAwait(false);

            return await DeserializeAsync<ConsumablesDetailsResponse>(consumablesDetailsResponseMessage);
        }
    }
}
