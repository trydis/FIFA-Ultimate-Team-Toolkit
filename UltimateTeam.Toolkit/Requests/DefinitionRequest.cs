using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class DefinitionRequest : FutRequestBase, IFutRequest<DefinitionResponse>
    {
        private readonly long _baseId;

        public DefinitionRequest(long baseId)
        {
            _baseId = baseId;
        }

        public async Task<DefinitionResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + string.Format(Resources.Definition, _baseId);
            Task<HttpResponseMessage> definitionResponseTask;

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Get);
                definitionResponseTask = HttpClient.PostAsync(uriString, new StringContent(" "));
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"&_={DateTime.Now.ToUnixTime()}";
                definitionResponseTask = HttpClient.GetAsync(uriString);
            }

            return await DeserializeAsync<DefinitionResponse>(await definitionResponseTask.ConfigureAwait(false));
        }
    }
}