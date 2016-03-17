using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class NationImageRequest : FutRequestBase, IFutRequest<byte[]>
    {
        private readonly Item _auctionInfo;
        private AppVersion _appVersion;

        public NationImageRequest(Item auctionInfo)
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
                    .GetByteArrayAsync(string.Format(Resources.FlagsImage, _auctionInfo.NationId))
                    .ConfigureAwait(false);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddAnonymousMobileHeader();
                return await HttpClient
                    .GetByteArrayAsync(string.Format(Resources.FlagsImage, _auctionInfo.NationId))
                    .ConfigureAwait(false);
            }
            else
            {
                return null;
            }
        }
    }
}
