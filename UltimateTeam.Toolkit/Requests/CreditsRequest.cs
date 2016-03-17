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
        private AppVersion _appVersion;

        public async Task<CreditsResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddMethodOverrideHeader(HttpMethod.Get);
                AddCommonHeaders();
                var creditsResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.Credits))
                    .ConfigureAwait(false);

                return await Deserialize<CreditsResponse>(creditsResponseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                var creditsResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.Credits + "?_=" + DateTimeExtensions.ToUnixTime(DateTime.Now)))
                    .ConfigureAwait(false);

                return await Deserialize<CreditsResponse>(creditsResponseMessage);
            }
            else
            {
                return null;
            }
        }
    }
}
