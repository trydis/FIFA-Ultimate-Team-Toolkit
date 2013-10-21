using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

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
            
            AddMethodOverrideHeader(HttpMethod.Delete);
            AddCommonHeaders();
            var removeFromTradePileMessage = await HttpClient
                .PostAsync(uriString, new StringContent(" "))
                .ConfigureAwait(false);
            removeFromTradePileMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}