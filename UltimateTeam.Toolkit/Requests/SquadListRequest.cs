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
        private AppVersion _appVersion;

        public async Task<SquadListResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddMethodOverrideHeader(HttpMethod.Get);
                AddCommonHeaders();
                var squadListResponseMessage = await HttpClient
                    .GetAsync(Resources.FutHome + Resources.SquadList)
                    .ConfigureAwait(false);

                return await Deserialize<SquadListResponse>(squadListResponseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                var squadListResponseMessage = await HttpClient
                    .GetAsync(Resources.FutHome + Resources.SquadList + "?_=" + DateTimeExtensions.ToUnixTime(DateTime.Now))
                    .ConfigureAwait(false);

                return await Deserialize<SquadListResponse>(squadListResponseMessage);
            }
            else
            {
                return null;
            }
        }
    }
}
