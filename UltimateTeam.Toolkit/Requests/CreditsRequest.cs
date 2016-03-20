using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class CreditsRequest : FutRequestBase, IFutRequest<CreditsResponse>
    {
        public async Task<CreditsResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.Credits;

            if (AppVersion == AppVersion.WebApp)
            {
                AddMethodOverrideHeader(HttpMethod.Get);
                AddCommonHeaders();
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"?_={DateTime.Now.ToUnixTime()}";
            }

            var creditsResponseMessage = await HttpClient
                .GetAsync(uriString)
                .ConfigureAwait(false);

            return await Deserialize<CreditsResponse>(creditsResponseMessage);
        }
    }
}
