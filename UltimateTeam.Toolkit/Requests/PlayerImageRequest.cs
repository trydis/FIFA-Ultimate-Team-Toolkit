using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

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
            AddUserAgent();
            AddAcceptHeader("*/*");
            AddReferrerHeader(Resources.BaseShowoff);
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();

            return await HttpClient
                .GetByteArrayAsync(string.Format(Resources.PlayerImage, _auctionInfo.CalculateBaseId()))
                .ConfigureAwait(false);
        }
    }
}
