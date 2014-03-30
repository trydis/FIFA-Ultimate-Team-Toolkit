using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SquadDetailRequest : FutRequestBase, IFutRequest<SquadDetailResponse>
    {
        private readonly ushort _squadId;

        public SquadDetailRequest(ushort SquadId)
        {
            SquadId.ThrowIfNullArgument();
            _squadId = SquadId;
        }

        public async Task<SquadDetailResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var squadResponseMessage = await HttpClient
                .GetAsync(string.Format(Resources.FutHome + Resources.SquadDetail, _squadId))
                .ConfigureAwait(false);

            return await Deserialize<SquadDetailResponse>(squadResponseMessage);
        }
    }
}
