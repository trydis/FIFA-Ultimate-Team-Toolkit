using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class QuickSellRequest : FutRequestBase, IFutRequest<QuickSellResponse>
    {
        private readonly IEnumerable<long> _itemIds;

        public QuickSellRequest(IEnumerable<long> itemIds)
        {
            _itemIds = itemIds;
        }

        public async Task<QuickSellResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Delete);
            AddCommonHeaders();

            var pathAndQuery = string.Format(Resources.FutHome + Resources.QuickSell, string.Join("%2C", _itemIds));
            var quickSellResponse = await HttpClient
                .PostAsync(pathAndQuery, new StringContent(" "))
                .ConfigureAwait(false);

            return await Deserialize<QuickSellResponse>(quickSellResponse);
        }
    }
}
