using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class RemoveFromTradePileRequest : FutRequestBase, IFutRequest<byte>
    {
        private readonly AuctionInfo _auctioninfo;

        public RemoveFromTradePileRequest(AuctionInfo auctioninfo)
        {
            auctioninfo.ThrowIfNullArgument();
            _auctioninfo = auctioninfo;
        }

        public async Task<byte> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.RemoveFromTradePile, _auctioninfo.TradeId);
            Task<HttpResponseMessage> removeFromTradePileMessageTask;

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Delete);
                removeFromTradePileMessageTask = HttpClient.PostAsync(uriString, new StringContent(" "));
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"?_={DateTime.Now.ToUnixTime()}";
                removeFromTradePileMessageTask = HttpClient.DeleteAsync(uriString);

            }

            var removeFromTradePileMessage = await removeFromTradePileMessageTask.ConfigureAwait(false);
            removeFromTradePileMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}