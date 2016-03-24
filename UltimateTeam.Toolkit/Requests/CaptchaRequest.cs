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
        public async Task<CaptchaResponse> PerformRequestAsync()
        {
            if (AppVersion != AppVersion.WebApp)
            {
                throw new FutException($"Not implemented for {AppVersion}");
            }

            AddLoginHeaders();
            var captchaResponse = await HttpClient
                .GetAsync(string.Format(Resources.CaptchaImage, DateTime.Now.ToUnixTime()))
                .ConfigureAwait(false);

            return await DeserializeAsync<CaptchaResponse>(captchaResponse);
        }
    }
}
