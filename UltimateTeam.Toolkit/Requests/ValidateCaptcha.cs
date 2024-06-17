using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ValidateCaptcha : FutRequestBase, IFutRequest<byte>
    {
        private readonly int _answer;
        private readonly AppVersion _appVersion;

        public ValidateCaptcha(int answer)
        {
            _answer = answer;
        }

        public async Task<byte> PerformRequestAsync()
        {
            if (_appVersion == AppVersion.WebApp)
            {
                //AddCaptchaHeaders(); ???
                //AddMethodOverrideHeader(HttpMethod.Post); ???
            }
            else
            {
                //AddCommonMobileHeaders(); ???
            }

            var content = @"{""token"":""AAAA"",""answer"":""" + _answer + @"""}";
            var responseMessage = await HttpClient
               .PostAsync(string.Format(Resources.CaptchaValidate), new StringContent(content))
               .ConfigureAwait(false);
            responseMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}
