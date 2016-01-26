using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PriceRangesRequest : FutRequestBase, IFutRequest<List<PriceRange>>
    {
        private readonly IEnumerable<long> _itemIds;

        public PriceRangesRequest(IEnumerable<long> itemIds)
        {
            itemIds.ThrowIfNullArgument();
            _itemIds = itemIds;
        }

        public async Task<List<PriceRange>> PerformRequestAsync()
        {
            AddCommonHeaders();
            AddMethodOverrideHeader(HttpMethod.Get);
            var priceRangesResponseMessage = await HttpClient
                .PostAsync(string.Format(Resources.FutHome + Resources.PriceRange, string.Join(",", _itemIds)), new StringContent(" ", Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);

            return await Deserialize<List<PriceRange>>(priceRangesResponseMessage);
        }
    }
}
