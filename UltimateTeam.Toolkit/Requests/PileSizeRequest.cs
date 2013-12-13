using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PileSizeRequest : FutRequestBase, IFutRequest<PileSizeResponse>
    {
        public async Task<PileSizeResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var creditsResponseMessage = await HttpClient
                .GetAsync(string.Format(Resources.FutHome + Resources.PileSize))
                .ConfigureAwait(false);

            return await Deserialize<PileSizeResponse>(creditsResponseMessage);
        }
    }
}