using System.Net;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Models.Auction;
using UltimateTeam.Toolkit.Models.Auth;
using UltimateTeam.Toolkit.Models.Player;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Services;

namespace UltimateTeam.Toolkit.Factories
{
    public class FutClient : IFutClient
    {
        public FutRequestFactories RequestFactories { get; }

        public FutClient()
        {
            RequestFactories = new FutRequestFactories();
        }

        public FutClient(CookieContainer cookieContainer)
        {
            RequestFactories = new FutRequestFactories(cookieContainer);
        }

        public async Task<LoginResponse> LoginAsync(LoginDetails loginDetails, ITwoFactorCodeProvider twoFactorCodeProvider)
        {
            loginDetails.ThrowIfNullArgument();

            var loginRequest = RequestFactories.LoginRequestFactory(loginDetails, twoFactorCodeProvider);
            RequestFactories.LoginResponse = await loginRequest.PerformRequestAsync();
            RequestFactories.LoginDetails = loginDetails;

            return RequestFactories.LoginResponse;
        }

        public Task<AuctionResponse> SearchAsync(SearchParameters searchParameters)
        {
            searchParameters.ThrowIfNullArgument();

            return RequestFactories.SearchRequestFactory(searchParameters).PerformRequestAsync();
        }

        public Task<AuctionResponse> PlaceBidAsync(AuctionInfo auctionInfo, uint bidAmount = 0)
        {
            auctionInfo.ThrowIfNullArgument();

            if (bidAmount == 0)
            {
                bidAmount = auctionInfo.CalculateBid();
            }

            return RequestFactories.PlaceBidRequestFactory(auctionInfo, bidAmount).PerformRequestAsync();
        }

        public Task<byte[]> GetPlayerImageAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return RequestFactories.PlayerImageRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task<AuctionResponse> GetTradeStatusAsync(IEnumerable<long> tradeIds)
        {
            tradeIds.ThrowIfNullArgument();

            return RequestFactories.TradeStatusRequestFactory(tradeIds).PerformRequestAsync();
        }

        public Task<CreditsResponse> GetCreditsAsync()
        {
            return RequestFactories.CreditsRequestFactory().PerformRequestAsync();
        }

        public Task<ConsumablesResponse> GetConsumablesAsync()
        {
            return RequestFactories.ConsumablesRequestFactory().PerformRequestAsync();
        }

        public Task<AuctionResponse> GetTradePileAsync()
        {
            return RequestFactories.TradePileRequestFactory().PerformRequestAsync();
        }

        public Task<WatchlistResponse> GetWatchlistAsync()
        {
            return RequestFactories.WatchlistRequestFactory().PerformRequestAsync();
        }

        public Task<ClubItemResponse> GetClubItemsAsync()
        {
            return RequestFactories.ClubItemRequestFactory().PerformRequestAsync();
        }

        public Task<SquadListResponse> GetSquadListAsync()
        {
            return RequestFactories.SquadListRequestFactory().PerformRequestAsync();
        }

        public Task<PurchasedItemsResponse> GetPurchasedItemsAsync()
        {
            return RequestFactories.PurchasedItemsRequestFactory().PerformRequestAsync();
        }

        public Task<ListAuctionResponse> ListAuctionAsync(AuctionDetails auctionDetails)
        {
            auctionDetails.ThrowIfNullArgument();

            return RequestFactories.ListAuctionFactory(auctionDetails).PerformRequestAsync();
        }

        public Task AddToWatchlistRequestAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return RequestFactories.AddToWatchlistRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task RemoveFromWatchlistAsync(IEnumerable<AuctionInfo> auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return RequestFactories.RemoveFromWatchlistRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task RemoveFromWatchlistAsync(AuctionInfo auctionInfo)
        {
            return RemoveFromWatchlistAsync(new[] { auctionInfo });
        }

        public Task RemoveFromTradePileAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return RequestFactories.RemoveFromTradePileRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task<SquadDetailsResponse> GetSquadDetailsAsync(ushort squadId)
        {
            return RequestFactories.SquadDetailsRequestFactory(squadId).PerformRequestAsync();
        }

        public Task<SendToClubResponse> SendToClubAsync(IEnumerable<long> itemIds)
        {
            itemIds.ThrowIfNullArgument();

            return RequestFactories.SendToClubRequestFactory(itemIds).PerformRequestAsync();
        }

        public Task<SendToClubResponse> SendToClubAsync(ItemData itemData, AuctionInfo auctionInfo)
        {
            itemData.ThrowIfNullArgument();

            return RequestFactories.SendTradeItemToClubRequestFactory(itemData, auctionInfo).PerformRequestAsync();
        }

        public Task<SendToTradePileResponse> SendToTradePileAsync(IEnumerable<long> itemIds)
        {
            itemIds.ThrowIfNullArgument();

            return RequestFactories.SendToTradePileRequestFactory(itemIds).PerformRequestAsync();
        }

        public Task<SendToTradePileResponse> SendToTradePileAsync(ItemData itemData, AuctionInfo auctionInfo)
        {
            itemData.ThrowIfNullArgument();

            return RequestFactories.SendTradeItemToTradePileRequestFactory(itemData, auctionInfo).PerformRequestAsync();
        }

        public Task<QuickSellResponse> QuickSellItemAsync(long itemId)
        {
            if (itemId < 1) throw new ArgumentException("ItemId not valid");

            return QuickSellItemAsync(new[] { itemId });
        }

        public Task<QuickSellResponse> QuickSellItemAsync(IEnumerable<long> itemIds)
        {
            if (itemIds == null) throw new ArgumentNullException(nameof(itemIds));

            foreach (var itemId in itemIds.Where(itemId => itemId < 1))
            {
                throw new ArgumentException($"ItemId {itemId} is definitely not valid", nameof(itemIds));
            }

            return RequestFactories.QuickSellRequestFactory(itemIds).PerformRequestAsync();
        }

        public Task<byte[]> GetClubImageAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return RequestFactories.ClubImageRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task<DefinitionResponse> GetDefinitionsAsync(long baseId)
        {
            return RequestFactories.DefinitionRequestFactory(baseId).PerformRequestAsync();
        }

        public Task<byte[]> GetNationImageAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return RequestFactories.NationImageRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task<RelistResponse> ReListAsync()
        {
            return RequestFactories.ReListRequestFactory().PerformRequestAsync();
        }

        public Task RemoveSoldItemsFromTradePileAsync()
        {
            return RequestFactories.RemoveSoldItemsFromTradePileRequestFactory().PerformRequestAsync();
        }

        public Task<StoreResponse> GetPackDetailsAsync()
        {
            return RequestFactories.GetPackDetailsFactory().PerformRequestAsync();
        }

        public Task<PurchasedPackResponse> BuyPackAsync(int packId, CurrencyOption currencyOption)
        {
            return RequestFactories.BuyPackRequestFactory(packId, currencyOption).PerformRequestAsync();
        }
    }
}
