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
        private AppVersion _appVersion;

        public QuickSellRequest(IEnumerable<long> itemIds)
        {
            _itemIds = itemIds;
        }

        public async Task<QuickSellResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddMethodOverrideHeader(HttpMethod.Delete);
                AddCommonHeaders();

                var pathAndQuery = string.Format(Resources.FutHome + Resources.QuickSell, string.Join("%2C", _itemIds));
                var quickSellResponse = await HttpClient
                    .PostAsync(pathAndQuery, new StringContent(" "))
                    .ConfigureAwait(false);

                return await Deserialize<QuickSellResponse>(quickSellResponse);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();

                var pathAndQuery = string.Format(Resources.FutHome + Resources.QuickSell + "&_=" + DateTimeExtensions.ToUnixTime(DateTime.Now), string.Join("%2C", _itemIds));
                var quickSellResponse = await HttpClient
                    .DeleteAsync(pathAndQuery)
                    .ConfigureAwait(false);

                return await Deserialize<QuickSellResponse>(quickSellResponse);
            }
            else
            {
                return null;
            }
        }
    }
}
