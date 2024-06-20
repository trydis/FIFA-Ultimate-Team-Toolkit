using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ConsumablesRequest : FutRequestBase, IFutRequest<ConsumablesResponse>
    {
        public async Task<ConsumablesResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.Consumables;
            AddCommonHeaders();

            var consumablesResponseMessage = await HttpClient
                .GetAsync(string.Format(uriString))
                .ConfigureAwait(false);

            return await DeserializeAsync<ConsumablesResponse>(consumablesResponseMessage);
        }
    }
}
