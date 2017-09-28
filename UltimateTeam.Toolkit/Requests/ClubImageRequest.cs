using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ClubImageRequest : FutRequestBase, IFutRequest<byte[]>
    {
        private readonly AuctionInfo _auctionInfo;

        public ClubImageRequest(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();
            _auctionInfo = auctionInfo;
        }

        public async Task<byte[]> PerformRequestAsync()
        {
            AddAnonymousHeader("https://www.easports.com/de/fifa/ultimate-team/web-app/");

            return await HttpClient
                .GetByteArrayAsync(string.Format(Resources.ClubImage, _auctionInfo.ItemData.TeamId))
                .ConfigureAwait(false);
        }
    }
}
