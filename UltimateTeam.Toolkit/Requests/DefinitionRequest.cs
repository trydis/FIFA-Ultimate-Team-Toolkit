using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.RequestFactory;

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

            AddCommonHeaders();
            definitionResponseTask = HttpClient.GetAsync(uriString);

            return await DeserializeAsync<DefinitionResponse>(await definitionResponseTask.ConfigureAwait(false));
        }
    }
}