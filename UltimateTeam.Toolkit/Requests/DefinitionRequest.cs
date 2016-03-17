using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Extensions;
using System;

namespace UltimateTeam.Toolkit.Requests
{
    internal class DefinitionRequest : FutRequestBase, IFutRequest<DefinitionResponse>
    {
        private readonly long _baseId;
        private AppVersion _appVersion;

        public DefinitionRequest(long baseId)
        {
            _baseId = baseId;
        }

        public async Task<DefinitionResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddMethodOverrideHeader(HttpMethod.Get);
                AddCommonHeaders();
                var uriString = string.Format(Resources.FutHome + Resources.Definition, _baseId);
                var definitionResponseMessage = await HttpClient
                    .PostAsync(uriString, new StringContent(" "))
                    .ConfigureAwait(false);

                return await Deserialize<DefinitionResponse>(definitionResponseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                var uriString = string.Format(Resources.FutHome + Resources.Definition + "&_=" + DateTimeExtensions.ToUnixTime(DateTime.Now), _baseId);
                var definitionResponseMessage = await HttpClient
                    .GetAsync(uriString)
                    .ConfigureAwait(false);

                return await Deserialize<DefinitionResponse>(definitionResponseMessage);
            }
            else
            {
                return null;
            }
        }
    }
}