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
        private AppVersion _appVersion;
        public async Task<ConsumablesResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddCommonHeaders();
                AddMethodOverrideHeader(HttpMethod.Get);
                var consumablesResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.Consumables))
                    .ConfigureAwait(false);

                return await Deserialize<ConsumablesResponse>(consumablesResponseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                var consumablesResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.Consumables + "?_=" + DateTimeExtensions.ToUnixTime(DateTime.Now)))
                    .ConfigureAwait(false);

                return await Deserialize<ConsumablesResponse>(consumablesResponseMessage);
            }
            else
            {
                return null;
            }
        }
    }
}
