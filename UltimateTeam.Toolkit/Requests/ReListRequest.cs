using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ReListRequest : FutRequestBase, IFutRequest<byte>
    {
        private AppVersion _appVersion;

        public async Task<byte> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                var uriString = string.Format(Resources.FutHome + Resources.Auctionhouse + "/" + Resources.ReList);

                AddMethodOverrideHeader(HttpMethod.Put);
                AddCommonHeaders();
                var reListMessage = await HttpClient
                    .PostAsync(uriString, new StringContent(" "))
                    .ConfigureAwait(false);
                reListMessage.EnsureSuccessStatusCode();

                return 0;
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                var uriString = string.Format(Resources.FutHome + Resources.Auctionhouse + "/" + Resources.ReList + "?_=" + DateTimeExtensions.ToUnixTime(DateTime.Now));

                AddCommonMobileHeaders();
                var reListMessage = await HttpClient
                    .PutAsync(uriString, new StringContent(" "))
                    .ConfigureAwait(false);
                reListMessage.EnsureSuccessStatusCode();

                return 0;
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", appVersion.ToString()));
            }
        }
    }
}
