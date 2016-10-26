using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class GiftRequest : FutRequestBase, IFutRequest<byte>
    {
        private readonly int _idGift;

        public GiftRequest(int idGift)
        {
            _idGift = idGift;
        }

        public async Task<byte> PerformRequestAsync()
        {
            if (AppVersion != AppVersion.WebApp)
            {
                throw new FutException($"Not implemented for {AppVersion}");
            }

            AddCommonHeaders(HttpMethod.Delete);
            var activeMessageRedeemResponseMessage = await HttpClient.PostAsync(
                string.Format(Resources.FutHome + Resources.ActiveMessageGet, _idGift),
                new StringContent(" ")).ConfigureAwait(false);

            activeMessageRedeemResponseMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}
