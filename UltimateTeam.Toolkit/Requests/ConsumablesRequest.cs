using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ConsumablesRequest : FutRequestBase, IFutRequest<ConsumablesResponse>
    {
        public async Task<ConsumablesResponse> PerformRequestAsync()
        {
            AddCommonHeaders();
            AddMethodOverrideHeader(HttpMethod.Get);
            var consumablesResponseMessage = await HttpClient
                .GetAsync(string.Format(Resources.FutHome + Resources.Consumables))
                .ConfigureAwait(false);

            return await Deserialize<ConsumablesResponse>(consumablesResponseMessage);
        }
    }
}
