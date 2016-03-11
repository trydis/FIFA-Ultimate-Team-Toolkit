using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;
using System;
using UltimateTeam.Toolkit.Exceptions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class RemoveFromTradePileRequest : FutRequestBase, IFutRequest<byte>
    {
        private readonly AuctionInfo _auctioninfo;
        private AppVersion _appVersion;

        public RemoveFromTradePileRequest(AuctionInfo auctioninfo)
        {
            auctioninfo.ThrowIfNullArgument();
            _auctioninfo = auctioninfo;
        }

        public async Task<byte> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                var uriString = string.Format(Resources.FutHome + Resources.RemoveFromTradePile, _auctioninfo.TradeId);

                AddMethodOverrideHeader(HttpMethod.Delete);
                AddCommonHeaders();
                var removeFromTradePileMessage = await HttpClient
                    .PostAsync(uriString, new StringContent(" "))
                    .ConfigureAwait(false);
                removeFromTradePileMessage.EnsureSuccessStatusCode();

                return 0;
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                var uriString = string.Format(Resources.FutHome + Resources.RemoveFromTradePile, _auctioninfo.TradeId + "?_=" + DateTimeExtensions.ToUnixTime(DateTime.Now));

                AddCommonMobileHeaders();
                var removeFromTradePileMessage = await HttpClient
                    .DeleteAsync(uriString)
                    .ConfigureAwait(false);
                removeFromTradePileMessage.EnsureSuccessStatusCode();

                return 0;
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", appVersion.ToString()));
            }
        }
    }
}