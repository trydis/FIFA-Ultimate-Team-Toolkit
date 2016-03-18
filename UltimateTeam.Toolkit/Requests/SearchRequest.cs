using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Extensions;
using System;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SearchRequest : FutRequestBase, IFutRequest<AuctionResponse>
    {
        private readonly SearchParameters _searchParameters;
        private AppVersion _appVersion;

        public SearchRequest(SearchParameters searchParameters)
        {
            searchParameters.ThrowIfNullArgument();
            _searchParameters = searchParameters;
        }

        public async Task<AuctionResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddCommonHeaders();
                AddMethodOverrideHeader(HttpMethod.Get);
                var uriString = string.Format(Resources.FutHome + Resources.TransferMarket + "?start={0}&num={1}",
                (_searchParameters.Page - 1) * _searchParameters.PageSize, _searchParameters.PageSize + 1);
                _searchParameters.BuildUriString(ref uriString);

                var searchResponseMessage = await HttpClient
                    .PostAsync(uriString, new StringContent(" "))
                    .ConfigureAwait(false);

                return await Deserialize<AuctionResponse>(searchResponseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                var uriString = string.Format(Resources.FutHome + Resources.TransferMarket + "?start={0}&num={1}",
                (_searchParameters.Page - 1) * _searchParameters.PageSize, _searchParameters.PageSize + 1);
                _searchParameters.BuildUriString(ref uriString);

                var searchResponseMessage = await HttpClient
                    .GetAsync(uriString + "&=" + DateTimeExtensions.ToUnixTime(DateTime.Now))
                    .ConfigureAwait(false);

                return await Deserialize<AuctionResponse>(searchResponseMessage);
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", appVersion.ToString()));
            }
        }
    }
}