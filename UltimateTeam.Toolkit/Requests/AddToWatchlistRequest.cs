using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace UltimateTeam.Toolkit.Requests
{
     internal class AddToWatchlistRequest : FutRequestBase, IFutRequest<byte>
    {

         private readonly IEnumerable<AuctionInfo> _auctioninfo;

         public AddToWatchlistRequest(IEnumerable<AuctionInfo> auctioninfo)
        {
            auctioninfo.ThrowIfNullArgument();
            _auctioninfo = auctioninfo;
        }

         // Request	POST ut/game/fifa14/watchlist?tradeId=140174434046
         // {"auctionInfo":[{"id":140196790669}]}

         public async Task<byte> PerformRequestAsync()
         {
             var tradeIds = string.Join("%2C", _auctioninfo.Select(p => p.TradeId));
             var uriString = string.Format(Resources.FutHome + Resources.Watchlist + "?tradeId={0}", tradeIds);
             var content = string.Format("{{\"auctionInfo\":[{{\"id\":{0}}}]}}", tradeIds);
             AddMethodOverrideHeader(HttpMethod.Put);
             AddCommonHeaders();
             var addToWatchlistResponseMessage = await HttpClient
                 .PostAsync(uriString, new StringContent(content))
                 .ConfigureAwait(false);
             addToWatchlistResponseMessage.EnsureSuccessStatusCode();

             return 0;
         }

    }
}
