using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PileSizeRequest : FutRequestBase, IFutRequest<PileSizeResponse>
    {
        private AppVersion _appVersion;

        public async Task<PileSizeResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddMethodOverrideHeader(HttpMethod.Get);
                AddCommonHeaders();
                var creditsResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.PileSize))
                    .ConfigureAwait(false);

                return await Deserialize<PileSizeResponse>(creditsResponseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                var creditsResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.PileSize + "?_=" + DateTimeExtensions.ToUnixTime(DateTime.Now)))
                    .ConfigureAwait(false);

                return await Deserialize<PileSizeResponse>(creditsResponseMessage);
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", appVersion.ToString()));
            }
        }
    }
}