using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

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
            var uriString = string.Format(Resources.FutHome + Resources.PriceRange, string.Join(",", _itemIds));
            Task<HttpResponseMessage> priceRangesResponseMessageTask;

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Get);
                priceRangesResponseMessageTask = HttpClient.PostAsync(uriString, new StringContent(" "));
            }
            else
            {
                AddCommonMobileHeaders();
                priceRangesResponseMessageTask = HttpClient.GetAsync(uriString + $"&_={DateTime.Now.ToUnixTime()}");
            }

            var priceRangesResponseMessage = await priceRangesResponseMessageTask.ConfigureAwait(false);

            return await DeserializeAsync<List<PriceRange>>(priceRangesResponseMessage);
        }
    }
}