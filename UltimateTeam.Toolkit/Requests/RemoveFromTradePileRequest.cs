using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models.Auction;
using UltimateTeam.Toolkit.RequestFactory;

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

            AddCommonHeaders();
            removeFromTradePileMessageTask = HttpClient.DeleteAsync(uriString);

            var removeFromTradePileMessage = await removeFromTradePileMessageTask.ConfigureAwait(false);
            removeFromTradePileMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}