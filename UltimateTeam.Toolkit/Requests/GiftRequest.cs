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
            AddCommonHeaders();
            var activeMessageRedeemResponseMessage = await HttpClient.DeleteAsync(string.Format(Resources.FutHome + Resources.ActiveMessageGet, _idGift)).ConfigureAwait(false);
            activeMessageRedeemResponseMessage.EnsureSuccessStatusCode();
            
            return 0;
        }
    }
}
