using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SquadListRequest : FutRequestBase, IFutRequest<SquadListResponse>
    {
        public async Task<SquadListResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var squadListResponseMessage = await HttpClient
                .GetAsync(Resources.FutHome + Resources.SquadList)
                .ConfigureAwait(false);

            return await Deserialize<SquadListResponse>(squadListResponseMessage);
        }
    }
}
