using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ReListRequest : FutRequestBase, IFutRequest<byte>
    {

        public async Task<byte> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.Auctionhouse + "/" + Resources.ReList);
            
            AddMethodOverrideHeader(HttpMethod.Put);
            AddCommonHeaders();
            var ReListMessage = await HttpClient
                .PostAsync(uriString, new StringContent(" "))
                .ConfigureAwait(false);
            ReListMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}
