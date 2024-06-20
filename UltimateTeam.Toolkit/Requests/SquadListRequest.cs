using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SquadListRequest : FutRequestBase, IFutRequest<SquadListResponse>
    {
        public async Task<SquadListResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.SquadList;

            AddCommonHeaders();
            var squadListResponseMessage = await HttpClient
                .GetAsync(uriString)
                .ConfigureAwait(false);

            return await DeserializeAsync<SquadListResponse>(squadListResponseMessage);
        }
    }
}
