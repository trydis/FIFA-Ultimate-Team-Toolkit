using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PlayerListRequest : FutRequestBase, IFutRequest<PlayerListResponse>
    {
        public async Task<PlayerListResponse> PerformRequestAsync()
        {
            var uriString = Resources.Players;
            AddCommonHeaders();

            var playerListResponseMessage = await HttpClient
                    .GetAsync(uriString)
                    .ConfigureAwait(false);

            return await DeserializeAsync<PlayerListResponse>(playerListResponseMessage);
        }
    }
}
