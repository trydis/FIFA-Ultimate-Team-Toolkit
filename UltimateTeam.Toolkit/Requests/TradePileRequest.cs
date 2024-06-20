using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class TradePileRequest : FutRequestBase, IFutRequest<AuctionResponse>
    {
        public async Task<AuctionResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.TradePile;
            AddCommonHeaders();

            var tradePileResponseMessage = await HttpClient
                .GetAsync(string.Format(uriString))
                .ConfigureAwait(false);

            return await DeserializeAsync<AuctionResponse>(tradePileResponseMessage);
        }
    }
}