using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Models.Auction;
using UltimateTeam.Toolkit.Models.Auth;
using UltimateTeam.Toolkit.Models.Player;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Services;

namespace UltimateTeam.Toolkit.Factories
{
    public interface IFutClient
    {
        FutRequestFactories RequestFactories { get; }

        Task<LoginResponse> LoginAsync(LoginDetails loginDetails, ITwoFactorCodeProvider twoFactorCodeProvider);

        Task<AuctionResponse> SearchAsync(SearchParameters searchParameters);

        Task<AuctionResponse> PlaceBidAsync(AuctionInfo auctionInfo, uint bidAmount = 0);

        Task<byte[]> GetPlayerImageAsync(AuctionInfo auctionInfo);

        Task<AuctionResponse> GetTradeStatusAsync(IEnumerable<long> tradeIds);

        Task<CreditsResponse> GetCreditsAsync();

        Task<AuctionResponse> GetTradePileAsync();

        Task<WatchlistResponse> GetWatchlistAsync();

        Task<ClubItemResponse> GetClubItemsAsync();

        Task<SquadListResponse> GetSquadListAsync();

        Task<SquadDetailsResponse> GetSquadDetailsAsync(ushort squadId);

        Task<PurchasedItemsResponse> GetPurchasedItemsAsync();

        Task<ListAuctionResponse> ListAuctionAsync(AuctionDetails auctionDetails);

        Task AddToWatchlistRequestAsync(AuctionInfo auctionInfo);

        Task RemoveFromWatchlistAsync(AuctionInfo auctionInfo);

        Task RemoveFromWatchlistAsync(IEnumerable<AuctionInfo> auctionInfo);

        Task RemoveFromTradePileAsync(AuctionInfo auctionInfo);

        Task<SendToClubResponse> SendToClubAsync(ItemData itemData, AuctionInfo auctionInfo);
        Task<SendToClubResponse> SendToClubAsync(IEnumerable<long> itemIds);

        Task<SendToTradePileResponse> SendToTradePileAsync(ItemData itemData, AuctionInfo auctionInfo);
        Task<SendToTradePileResponse> SendToTradePileAsync(IEnumerable<long> itemIds);

        Task<QuickSellResponse> QuickSellItemAsync(long itemId);

        Task<QuickSellResponse> QuickSellItemAsync(IEnumerable<long> itemId);

        Task<ConsumablesResponse> GetConsumablesAsync();

        Task<byte[]> GetClubImageAsync(AuctionInfo auctionInfo);

        Task<byte[]> GetNationImageAsync(AuctionInfo item);

        Task<RelistResponse> ReListAsync();

        Task<DefinitionResponse> GetDefinitionsAsync(long baseId);

        Task RemoveSoldItemsFromTradePileAsync();

        Task<StoreResponse> GetPackDetailsAsync();

        Task<PurchasedPackResponse> BuyPackAsync(int packId, CurrencyOption currency);
    }
}
