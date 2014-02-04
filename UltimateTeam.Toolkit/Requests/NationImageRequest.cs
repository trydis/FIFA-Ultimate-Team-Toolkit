using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

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
            AddUserAgent();
            AddAcceptHeader("*/*");
            AddReferrerHeader(Resources.BaseShowoff);
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();

            return await HttpClient
                .GetByteArrayAsync(string.Format(Resources.FlagsImage, _auctionInfo.NationId))
                .ConfigureAwait(false);
        }
    }
}
