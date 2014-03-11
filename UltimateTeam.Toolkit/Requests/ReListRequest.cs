using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ReListRequest : FutRequestBase, IFutRequest<byte>
    {
        public async Task<byte> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.Auctionhouse + "/" + Resources.ReList);

            AddMethodOverrideHeader(HttpMethod.Put);
            AddCommonHeaders();
            var reListMessage = await HttpClient
                .PostAsync(uriString, new StringContent(" "))
                .ConfigureAwait(false);
            reListMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}
