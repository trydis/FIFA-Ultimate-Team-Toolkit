using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
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
            var uriString = string.Format(Resources.FutHome + Resources.QuickSell, string.Join("%2C", _itemIds));
            Task<HttpResponseMessage> quickSellResponseTask;

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Delete);
                quickSellResponseTask = HttpClient.PostAsync(uriString, new StringContent(" "));
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"&_={DateTime.Now.ToUnixTime()}";
                quickSellResponseTask = HttpClient.DeleteAsync(uriString);
            }

            var quickSellResponse = await quickSellResponseTask.ConfigureAwait(false);

            return await DeserializeAsync<QuickSellResponse>(quickSellResponse);
        }
    }
}
