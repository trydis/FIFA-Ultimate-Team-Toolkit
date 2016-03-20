using System;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class CaptchaRequest : FutRequestBase, IFutRequest<CaptchaResponse>
    {
        private AppVersion _appVersion;

        public async Task<CaptchaResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddLoginHeaders();
                var captchaResponse = await HttpClient
                    .GetAsync(string.Format(Resources.CaptchaImage, DateTimeExtensions.ToUnixTime(DateTime.Now)))
                    .ConfigureAwait(false);

                return await Deserialize<CaptchaResponse>(captchaResponse);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                throw new FutException(string.Format("Not implemented via {0}", appVersion.ToString()));
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion ({0})", appVersion.ToString()));
            }
        }
    }
}
