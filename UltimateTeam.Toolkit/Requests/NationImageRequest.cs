using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class NationImageRequest : FutRequestBase, IFutRequest<byte[]>
    {
        private readonly Item _auctionInfo;

        public NationImageRequest(Item auctionInfo)
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
                .GetByteArrayAsync(string.Format(Resources.FlagsImage, _auctionInfo.NationId))
                .ConfigureAwait(false);
        }
    }
}
