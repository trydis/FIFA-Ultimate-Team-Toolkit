using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ListGiftsRequest : FutRequestBase, IFutRequest<ListGiftsResponse>
    {
        private AppVersion _appVersion;

        public async Task<ListGiftsResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddCommonHeaders();
                AddMethodOverrideHeader(HttpMethod.Get);
                var responseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.ActiveMessageList))
                    .ConfigureAwait(false);

                return await Deserialize<ListGiftsResponse>(responseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                //I don't know if Gifts are available via CompanionApp
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
