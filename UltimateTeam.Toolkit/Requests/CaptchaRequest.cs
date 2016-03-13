using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class CaptchaRequest : FutRequestBase, IFutRequest<CaptchaResponse>
    {
        public async Task<CaptchaResponse> PerformRequestAsync()
        {
            AddLoginHeaders();
            var captchaResponse = await HttpClient
                .GetAsync(string.Format(Resources.CaptchaImage, CreateTimestamp()))
                .ConfigureAwait(false);

            return await Deserialize<CaptchaResponse>(captchaResponse);
        }

        private static long CreateTimestamp()
        {
            var duration = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);

            return ((long)(1000 * duration.TotalSeconds));
        }
    }
}
