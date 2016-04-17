using System;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
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
                AddMobileCaptchaHeaders();
                var captchaResponseImage = await HttpClient
                    .GetAsync(string.Format(Resources.CaptchaImage, DateTimeExtensions.ToUnixTime(DateTime.Now)))
                    .ConfigureAwait(false);

                CaptchaResponse companionAppCaptchaResponse = new CaptchaResponse();
                companionAppCaptchaResponse.EncodedImg = Convert.ToBase64String(captchaResponseImage.Content.ReadAsByteArrayAsync().Result);
                companionAppCaptchaResponse.SizebeforeEncode = Convert.ToUInt32(captchaResponseImage.Content.ReadAsByteArrayAsync().Result.Length);

                return companionAppCaptchaResponse;
            }

            AddCaptchaHeaders();
            var webAppCaptchaResponse = await HttpClient
                .GetAsync(string.Format(Resources.CaptchaImage, DateTimeExtensions.ToUnixTime(DateTime.Now)))
                .ConfigureAwait(false);

            return await DeserializeAsync<CaptchaResponse>(webAppCaptchaResponse);
        }
    }
}
