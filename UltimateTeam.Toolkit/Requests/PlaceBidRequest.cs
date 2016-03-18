using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;
using System;
using UltimateTeam.Toolkit.Exceptions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PlaceBidRequest : FutRequestBase, IFutRequest<AuctionResponse>
    {
        private readonly AuctionInfo _auctionInfo;
        private AppVersion _appVersion;
        private readonly uint _bidAmount;

        public PlaceBidRequest(AuctionInfo auctionInfo, uint bidAmount)
        {
            auctionInfo.ThrowIfNullArgument();
            _auctionInfo = auctionInfo;
            _bidAmount = bidAmount;
        }

        public async Task<AuctionResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddMethodOverrideHeader(HttpMethod.Put);
                AddCommonHeaders();
                var content = string.Format("{{\"bid\":{0}}}", _bidAmount);
                var bidResponseMessage = await HttpClient
                    .PostAsync(string.Format(Resources.FutHome + Resources.Bid, _auctionInfo.TradeId), new StringContent(content))
                    .ConfigureAwait(false);

                return await Deserialize<AuctionResponse>(bidResponseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                var content = string.Format("{{\"bid\":{0}}}", _bidAmount);
                var bidResponseMessage = await HttpClient
                    .PutAsync(string.Format(Resources.FutHome + Resources.Bid + "?_=" + DateTimeExtensions.ToUnixTime(DateTime.Now), _auctionInfo.TradeId), new StringContent(content))
                    .ConfigureAwait(false);

                return await Deserialize<AuctionResponse>(bidResponseMessage);
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", appVersion.ToString()));
            }
        }
    }
}