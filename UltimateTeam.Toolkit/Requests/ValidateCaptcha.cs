using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ValidateCaptcha : FutRequestBase, IFutRequest<byte>
    {
        private readonly int _answer;
        private AppVersion _appVersion;

        public ValidateCaptcha(int answer)
        {
            _answer = answer;
        }

        public async Task<byte> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
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
