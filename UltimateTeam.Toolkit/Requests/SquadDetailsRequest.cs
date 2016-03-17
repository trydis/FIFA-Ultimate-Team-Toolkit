using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;
using System;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SquadDetailsRequest : FutRequestBase, IFutRequest<SquadDetailsResponse>
    {
        private readonly ushort _squadId;
        private readonly string _personaId;
        private AppVersion _appVersion;

        public SquadDetailsRequest(ushort squadId, string personaId)
        {
            squadId.ThrowIfNullArgument();
            _squadId = squadId;
            _personaId = personaId;
        }

        public async Task<SquadDetailsResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddMethodOverrideHeader(HttpMethod.Get);
                AddCommonHeaders();
                var squadResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.SquadDetails, _squadId))
                    .ConfigureAwait(false);

                return await Deserialize<SquadDetailsResponse>(squadResponseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                var squadResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.SquadDetails, _squadId + "/user/" + _personaId +"?_=" + DateTimeExtensions.ToUnixTime(DateTime.Now)))
                    .ConfigureAwait(false);

                return await Deserialize<SquadDetailsResponse>(squadResponseMessage);
            }
            else
            {
                return null;
            }
        }
    }
}
