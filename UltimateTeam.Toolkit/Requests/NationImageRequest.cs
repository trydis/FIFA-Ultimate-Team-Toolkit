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
            AddAnonymousHeader("https://www.easports.com/de/fifa/ultimate-team/web-app/");

            return await HttpClient
                .GetByteArrayAsync(string.Format(Resources.FlagsImage, _auctionInfo.NationId))
                .ConfigureAwait(false);
        }
    }
}
