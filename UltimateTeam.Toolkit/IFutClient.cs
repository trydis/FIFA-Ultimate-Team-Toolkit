using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;

namespace UltimateTeam.Toolkit
{
    public interface IFutClient
    {
        Task<LoginResponse> LoginAsync(LoginDetails loginDetails);

        Task<AuctionResponse> SearchAsync(SearchParameters searchParameters);

        Task<AuctionResponse> PlaceBidAsync(AuctionInfo auctionInfo, uint bidAmount = 0);

        Task<Item> GetItemAsync(AuctionInfo auctionInfo);

        Task<byte[]> GetPlayerImageAsync(AuctionInfo auctionInfo);

        Task<AuctionResponse> GetTradeStatusAsync(IEnumerable<long> tradeIds);

        Task<CreditsResponse> GetCreditsAsync();
    }
}
