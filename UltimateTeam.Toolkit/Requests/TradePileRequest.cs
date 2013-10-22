using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class TradePileRequest : FutRequestBase, IFutRequest<AuctionResponse>
    {
        public async Task<AuctionResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var tradePileResponseMessage = await HttpClient
                .GetAsync(string.Format(Resources.FutHome + Resources.TradePile))
                .ConfigureAwait(false);

            return await Deserialize<AuctionResponse>(tradePileResponseMessage);
        }
    }
}