using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

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
            AddMethodOverrideHeader(HttpMethod.Post);
            AddLoginHeaders();
            var content = @"{""token"":""AAAA"",""answer"":""" + _answer + @"""}";
            var captchaResponseMessage = await HttpClient
                .PostAsync(string.Format(Resources.CaptchaValidate), new StringContent(content))
                .ConfigureAwait(false);
            captchaResponseMessage.EnsureSuccessStatusCode();


            return 0;
        }
    }
}
