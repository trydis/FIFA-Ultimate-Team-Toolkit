using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class GiftRequest : FutRequestBase, IFutRequest<byte>
    {
        private readonly int _idGift;
        private AppVersion _appVersion;

        public GiftRequest(int idGift)
        {
            _idGift = idGift;
        }

        public async Task<byte> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddMethodOverrideHeader(HttpMethod.Delete);
                AddCommonHeaders();
                var activeMessageRedeemResponseMessage = await HttpClient.PostAsync(
                    string.Format(Resources.FutHome + Resources.ActiveMessageGet, _idGift),
                    new StringContent(" ")).ConfigureAwait(false);
                activeMessageRedeemResponseMessage.EnsureSuccessStatusCode();
                return 0;
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                //I don't know if Gifts are available via CompanionApp
                return 0;
            }
            else
            {
                return 0;
            }
        }
    }
}
