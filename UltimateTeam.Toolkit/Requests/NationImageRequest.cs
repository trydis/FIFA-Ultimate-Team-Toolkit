using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models.Auction;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class NationImageRequest : FutRequestBase, IFutRequest<byte[]>
    {
        private readonly AuctionInfo _auctionInfo;

        public NationImageRequest(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();
            _auctionInfo = auctionInfo;
        }

        public async Task<byte[]> PerformRequestAsync()
        {
            AddAnonymousHeader("https://www.easports.com/de/fifa/ultimate-team/web-app/");

            return await HttpClient
                .GetByteArrayAsync(string.Format(Resources.FlagsImage, _auctionInfo.ItemData.Nation))
                .ConfigureAwait(false);
        }
    }
}
