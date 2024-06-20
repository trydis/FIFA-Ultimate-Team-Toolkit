using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SquadDetailsRequest : FutRequestBase, IFutRequest<SquadDetailsResponse>
    {
        private readonly ushort _squadId;
        public SquadDetailsRequest(ushort squadId)
        {
            _squadId = squadId;
        }

        public async Task<SquadDetailsResponse> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.SquadDetails, _squadId);

            AddCommonHeaders();
            if (base.LoginResponse?.DefaultPersona?.PersonaId == null || base.LoginResponse.DefaultPersona.PersonaId <= 0)
                throw new Exception("PersonaId is not set");
            uriString += $"/user/{base.LoginResponse.DefaultPersona.PersonaId}";

            var squadResponseMessage = await HttpClient
                    .GetAsync(uriString)
                    .ConfigureAwait(false);

            return await DeserializeAsync<SquadDetailsResponse>(squadResponseMessage);
        }
    }
}
