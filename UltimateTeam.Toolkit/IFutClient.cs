using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Factories;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;

namespace UltimateTeam.Toolkit
{
    public interface IFutClient
    {
        FutRequestFactories RequestFactories { get; }

        Task<LoginResponse> LoginAsync(LoginDetails loginDetails);

        Task<AuctionResponse> SearchAsync(SearchParameters searchParameters);

        Task<AuctionResponse> PlaceBidAsync(AuctionInfo auctionInfo, uint bidAmount = 0);

        Task<Item> GetItemAsync(AuctionInfo auctionInfo);

        Task<byte[]> GetPlayerImageAsync(AuctionInfo auctionInfo);

        Task<AuctionResponse> GetTradeStatusAsync(IEnumerable<long> tradeIds);

        Task<CreditsResponse> GetCreditsAsync();

        Task<PileSizeResponse> GetPileSizeAsync();

        Task<AuctionResponse> GetTradePileAsync();

        Task<WatchlistResponse> GetWatchlistAsync();

        Task<PurchasedItemsResponse> GetPurchasedItemsAsync();

        Task<ListAuctionResponse> ListAuctionAsync(AuctionDetails auctionDetails);

        Task RemoveFromWatchlistAsync(AuctionInfo auctionInfo);

        Task RemoveFromWatchlistAsync(IEnumerable<AuctionInfo> auctionInfo);

        Task RemoveFromTradePileAsync(AuctionInfo auctionInfo);

        Task<SendItemToTradePileResponse> SendItemToTradePileAsync(ItemData itemData);

        Task<QuickSellResponse> QuickSellItemAsync(long itemId);

        Task<QuickSellResponse> QuickSellItemAsync(IEnumerable<long> itemId);
    }
}
