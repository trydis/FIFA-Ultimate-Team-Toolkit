using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Exceptions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ClubImageRequest : FutRequestBase, IFutRequest<byte[]>
    {
        private readonly AuctionInfo _auctionInfo;
        private AppVersion _appVersion;

        public ClubImageRequest(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();
            _auctionInfo = auctionInfo;
        }

        public async Task<byte[]> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddAnonymousHeader();
                return await HttpClient
                    .GetByteArrayAsync(string.Format(Resources.ClubImage, _auctionInfo.ItemData.TeamId))
                    .ConfigureAwait(false);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddAnonymousMobileHeader();
                return await HttpClient
                    .GetByteArrayAsync(string.Format(Resources.ClubImage, _auctionInfo.ItemData.TeamId))
                    .ConfigureAwait(false);
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", appVersion.ToString()));
            }
        }
    }
}
