using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ValidateCaptcha : FutRequestBase, IFutRequest<byte>
    {
        private readonly int _answer;

        public ValidateCaptcha(int answer)
        {
            _answer = answer;
        }

        public async Task<byte> PerformRequestAsync()
        {
            if (AppVersion != AppVersion.WebApp)
            {
                AddCommonMobileHeaders();
                var companionAppcontent = @"{""token"":""AAAA"",""answer"":""" + _answer + @"""}";
                var companionAppcaptchaResponseMessage = await HttpClient
                    .PostAsync(string.Format(Resources.CaptchaValidate), new StringContent(companionAppcontent))
                    .ConfigureAwait(false);
                companionAppcaptchaResponseMessage.EnsureSuccessStatusCode();

                return 0;
            }

            AddCaptchaHeaders();
            AddMethodOverrideHeader(HttpMethod.Post);
            var webAppcontent = @"{""token"":""AAAA"",""answer"":""" + _answer + @"""}";
            var webAppcaptchaResponseMessage = await HttpClient
                .PostAsync(string.Format(Resources.CaptchaValidate), new StringContent(webAppcontent))
                .ConfigureAwait(false);
            webAppcaptchaResponseMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}
