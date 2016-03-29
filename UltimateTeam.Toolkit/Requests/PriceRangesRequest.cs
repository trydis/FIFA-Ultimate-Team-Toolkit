using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;
using System;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PriceRangesRequest : FutRequestBase, IFutRequest<List<PriceRange>>
    {
        private readonly IEnumerable<long> _itemIds;
        private AppVersion _appVersion;

        public PriceRangesRequest(IEnumerable<long> itemIds)
        {
            itemIds.ThrowIfNullArgument();
            _itemIds = itemIds;
        }

        public async Task<List<PriceRange>> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddCommonHeaders();
                AddMethodOverrideHeader(HttpMethod.Get);
                var priceRangesResponseMessage = await HttpClient
                    .PostAsync(string.Format(Resources.FutHome + Resources.PriceRange, string.Join(",", _itemIds)), new StringContent(" "))
                    .ConfigureAwait(false);

                return await Deserialize<List<PriceRange>>(priceRangesResponseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                  var priceRangesResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.FutHome + Resources.PriceRange + "&_=" + DateTimeExtensions.ToUnixTime(DateTime.Now), string.Join(",", _itemIds)))
                    .ConfigureAwait(false);

                return await Deserialize<List<PriceRange>>(priceRangesResponseMessage);
            }
            else
            {
                return null;
            }
        }
    }
}
