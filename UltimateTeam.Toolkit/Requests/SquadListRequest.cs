using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SquadListRequest : FutRequestBase, IFutRequest<SquadListResponse>
    {
        public async Task<SquadListResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.SquadList;

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Get);
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"?_={DateTime.Now.ToUnixTime()}";
            }

            var squadListResponseMessage = await HttpClient
                .GetAsync(uriString)
                .ConfigureAwait(false);

            return await DeserializeAsync<SquadListResponse>(squadListResponseMessage);
        }
    }
}
