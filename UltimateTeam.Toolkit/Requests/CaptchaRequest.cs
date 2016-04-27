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
            if (AppVersion == AppVersion.WebApp)
            {
                AddCaptchaHeaders();
            }
            else
            {
                AddMobileCaptchaHeaders();
            }

            var responseMessage = await HttpClient
                  .GetAsync(string.Format(Resources.CaptchaImage, DateTime.Now.ToUnixTime()))
                  .ConfigureAwait(false);

            if (AppVersion == AppVersion.WebApp)
            {
                return await DeserializeAsync<CaptchaResponse>(responseMessage);
            }

            return new CaptchaResponse
            {
                EncodedImg = Convert.ToBase64String(await responseMessage.Content.ReadAsByteArrayAsync()),
                SizebeforeEncode = Convert.ToUInt32((await responseMessage.Content.ReadAsByteArrayAsync()).Length)
            };
        }
    }
}
