using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class QuickSellRequest : FutRequestBase, IFutRequest<QuickSellResponse>
    {
        private readonly long _itemId;

        public QuickSellRequest(long itemId)
        {
            itemId.ThrowIfNullArgument();
            _itemId = itemId;
        }

        public async Task<QuickSellResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Delete);
            AddCommonHeaders();
            var quickSellResponse = await HttpClient.PostAsync(string.Format(Resources.FutHome + Resources.QuickSell, _itemId), new StringContent(" "));
            quickSellResponse.EnsureSuccessStatusCode();

            return await Deserialize<QuickSellResponse>(quickSellResponse);
        }
    }
}
