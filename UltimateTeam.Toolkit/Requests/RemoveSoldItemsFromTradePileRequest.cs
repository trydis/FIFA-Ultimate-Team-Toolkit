using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class RemoveSoldItemsFromTradePileRequest : FutRequestBase, IFutRequest<byte>
    {
        private AppVersion _appVersion;

        public async Task<byte> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                var uriString = string.Format(Resources.FutHome + Resources.RemoveFromTradePile, "sold");

                AddMethodOverrideHeader(HttpMethod.Delete);
                AddCommonHeaders();
                var removeFromTradePileMessage = await HttpClient
                    .PostAsync(uriString, new StringContent(" "))
                    .ConfigureAwait(false);
                removeFromTradePileMessage.EnsureSuccessStatusCode();

                return 0;
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                var uriString = string.Format(Resources.FutHome + Resources.RemoveFromTradePile, "sold");

                AddCommonMobileHeaders();
                var removeFromTradePileMessage = await HttpClient
                    .DeleteAsync(uriString)
                    .ConfigureAwait(false);
                removeFromTradePileMessage.EnsureSuccessStatusCode();

                return 0;
            }
            else
            {
                return 0;
            }
        }
    }
}