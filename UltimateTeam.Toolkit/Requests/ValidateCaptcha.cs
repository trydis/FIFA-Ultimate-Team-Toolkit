using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;

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
                throw new FutException($"Not implemented for {AppVersion}");
            }

            AddMethodOverrideHeader(HttpMethod.Post);
            AddLoginHeaders();
            var content = @"{""token"":""AAAA"",""answer"":""" + _answer + @"""}";
            var captchaResponseMessage = await HttpClient
                .PostAsync(Resources.CaptchaValidate, new StringContent(content))
                .ConfigureAwait(false);

            captchaResponseMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}
