using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class TradeStatusRequest : FutRequestBase, IFutRequest<AuctionResponse>
    {
        private readonly IEnumerable<long> _tradeIds;

        public TradeStatusRequest(IEnumerable<long> tradeIds)
        {
            tradeIds.ThrowIfNullArgument();
            _tradeIds = tradeIds;
        }

        public async Task<AuctionResponse> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.TradeStatus, string.Join("%2C", _tradeIds));
            Task<HttpResponseMessage> tradeStatusResponseMessageTask;

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Get);
                tradeStatusResponseMessageTask = HttpClient.PostAsync(uriString, new StringContent(" "));
            }
            else
            {
                AddCommonMobileHeaders();
                tradeStatusResponseMessageTask = HttpClient.GetAsync(uriString + $"&_={DateTime.Now.ToUnixTime()}");
            }

            var tradeStatusResponseMessage = await tradeStatusResponseMessageTask.ConfigureAwait(false);

            return await DeserializeAsync<AuctionResponse>(tradeStatusResponseMessage);
        }
    }
}
