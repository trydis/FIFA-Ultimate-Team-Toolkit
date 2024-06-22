using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models.Auction;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PlayerImageRequest : FutRequestBase, IFutRequest<byte[]>
    {
        private readonly AuctionInfo _auctionInfo;

        public PlayerImageRequest(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();
            _auctionInfo = auctionInfo;
        }

        public async Task<byte[]> PerformRequestAsync()
        {
            AddAnonymousHeader("https://www.ea.com/ea-sports-fc/ultimate-team/web-app/");

            return await HttpClient
                             .GetByteArrayAsync(string.Format(Resources.PlayerImage, _auctionInfo.ItemData.AssetId))
                             .ConfigureAwait(false);
        }
    }
}
