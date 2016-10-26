using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SquadDetailsRequest : FutRequestBase, IFutRequest<SquadDetailsResponse>
    {
        private readonly ushort _squadId;
        private readonly string _personaId;

        public SquadDetailsRequest(ushort squadId, string personaId)
        {
            personaId.ThrowIfInvalidArgument();
            _squadId = squadId;
            _personaId = personaId;
        }

        public async Task<SquadDetailsResponse> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.SquadDetails, _squadId);

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Get);
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"/user/{_personaId}?_={DateTime.Now.ToUnixTime()}";
            }

            var squadResponseMessage = await HttpClient
                    .GetAsync(uriString)
                    .ConfigureAwait(false);

            return await DeserializeAsync<SquadDetailsResponse>(squadResponseMessage);
        }
    }
}
