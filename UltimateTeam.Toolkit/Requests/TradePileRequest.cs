using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class TradePileRequest : FutRequestBase, IFutRequest<TradePileResponse>
    {
        public async Task<TradePileResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var tradePileResponseMessage = await HttpClient.GetAsync(string.Format(Resources.FutHome + Resources.TradePile));
            tradePileResponseMessage.EnsureSuccessStatusCode();

            return await Deserialize<TradePileResponse>(tradePileResponseMessage);
        }
    }
}