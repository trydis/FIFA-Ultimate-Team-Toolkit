using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SquadDetailsRequest : FutRequestBase, IFutRequest<SquadDetailsResponse>
    {
        private readonly ushort _squadId;

        public SquadDetailsRequest(ushort squadId)
        {
            squadId.ThrowIfNullArgument();
            _squadId = squadId;
        }

        public async Task<SquadDetailsResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var squadResponseMessage = await HttpClient
                .GetAsync(string.Format(Resources.FutHome + Resources.SquadDetails, _squadId))
                .ConfigureAwait(false);

            return await Deserialize<SquadDetailsResponse>(squadResponseMessage);
        }
    }
}
