using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PlayerImageRequest : FutRequestBase, IFutRequest<byte[]>
    {
        private readonly AuctionInfo _auctionInfo;
        private AppVersion _appVersion;

        public PlayerImageRequest(AuctionInfo auctionInfo)
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
                    .GetByteArrayAsync(string.Format(Resources.PlayerImage, _auctionInfo.CalculateBaseId()))
                    .ConfigureAwait(false);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddAnonymousMobileHeader();
                return await HttpClient
                    .GetByteArrayAsync(string.Format(Resources.PlayerImage, _auctionInfo.CalculateBaseId()))
                    .ConfigureAwait(false);
            }
            else
            {
                return null;
            }
        }
    }
}
