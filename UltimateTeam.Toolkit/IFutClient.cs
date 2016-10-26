using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Factories;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Services;

namespace UltimateTeam.Toolkit
{
    public interface IFutClient
    {
        FutRequestFactories RequestFactories { get; }

        Task<LoginResponse> LoginAsync(LoginDetails loginDetails, ITwoFactorCodeProvider twoFactorCodeProvider);

        Task<AuctionResponse> SearchAsync(SearchParameters searchParameters);

        Task<AuctionResponse> PlaceBidAsync(AuctionInfo auctionInfo, uint bidAmount = 0);

        Task<Item> GetItemAsync(AuctionInfo auctionInfo);

        Task<Item> GetItemAsync(long resourceId);

        Task<byte[]> GetPlayerImageAsync(AuctionInfo auctionInfo);

        Task<AuctionResponse> GetTradeStatusAsync(IEnumerable<long> tradeIds);

        Task<CreditsResponse> GetCreditsAsync();

        Task<PileSizeResponse> GetPileSizeAsync();

        Task<AuctionResponse> GetTradePileAsync();

        Task<WatchlistResponse> GetWatchlistAsync();

        Task<ClubItemResponse> GetClubItemsAsync();

        Task<SquadListResponse> GetSquadListAsync();

        Task<SquadDetailsResponse> GetSquadDetailsAsync(ushort squadId);

        Task<PurchasedItemsResponse> GetPurchasedItemsAsync();

        Task<ListAuctionResponse> ListAuctionAsync(AuctionDetails auctionDetails);

        Task AddToWatchlistRequestAsync(AuctionInfo auctionInfo);

        Task AddToWatchlistRequestAsync(IEnumerable<AuctionInfo> auctionInfo);

        Task RemoveFromWatchlistAsync(AuctionInfo auctionInfo);

        Task RemoveFromWatchlistAsync(IEnumerable<AuctionInfo> auctionInfo);

        Task RemoveFromTradePileAsync(AuctionInfo auctionInfo);

        Task<SendItemToClubResponse> SendItemToClubAsync(ItemData itemData);

        Task<SendItemToTradePileResponse> SendItemToTradePileAsync(ItemData itemData);

        Task<QuickSellResponse> QuickSellItemAsync(long itemId);

        Task<QuickSellResponse> QuickSellItemAsync(IEnumerable<long> itemId);

        Task<ConsumablesResponse> GetConsumablesAsync();

        Task<byte[]> GetClubImageAsync(AuctionInfo auctionInfo);

        Task<byte[]> GetNationImageAsync(Item item);

        Task<RelistResponse> ReListAsync();

        Task<ListGiftsResponse> GetGiftsListAsync();

        Task GetGiftAsync(int idGift);

        Task<DefinitionResponse> GetDefinitionsAsync(long baseId);

        Task<List<PriceRange>> GetPriceRangesAsync(IEnumerable<long> itemIds);

        Task<CaptchaResponse> GetCaptchaAsync();

        Task<byte> ValidateCaptchaAsync(int answer);

        Task RemoveSoldItemsFromTradePileAsync();

        Task<StoreResponse> GetPackDetailsAsync();

        Task<PurchasedPackResponse> BuyPackAsync(PackDetails packDetails);
    }
}
