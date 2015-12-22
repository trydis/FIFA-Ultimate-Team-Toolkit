using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;

namespace UltimateTeam.Toolkit.Requests
{
    internal class GiftRequest : FutRequestBase, IFutRequest<int>
    {
        private readonly int _idGift;

        public GiftRequest(int idGift)
        {
            _idGift = idGift;
        }

        public async Task<int> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.Gifts + "/" + _idGift);

            AddMethodOverrideHeader(HttpMethod.Post);
            AddCommonHeaders();
            var responseMessage = await HttpClient
                .PostAsync(uriString, new StringContent(" "))
                .ConfigureAwait(false);
            //responseMessage.EnsureSuccessStatusCode();

            return _idGift;
        }
    }
}
