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
            if (AppVersion == AppVersion.WebApp)
            {
                AddAnonymousHeader();
            }
            else
            {
                AddAnonymousMobileHeader();
            }

            return await HttpClient
                .GetByteArrayAsync(string.Format(Resources.ClubImage, _auctionInfo.ItemData.TeamId))
                .ConfigureAwait(false);
        }
    }
}
