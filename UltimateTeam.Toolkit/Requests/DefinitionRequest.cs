using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Extensions;

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
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var uriString = string.Format(Resources.FutHome + Resources.Definition, _baseId);
            var definitionResponseMessage = await HttpClient
                .PostAsync(uriString, new StringContent(" "))
                .ConfigureAwait(false);

            return await Deserialize<DefinitionResponse>(definitionResponseMessage);
        }
    }
}