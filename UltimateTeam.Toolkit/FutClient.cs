using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Factories;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Services;

namespace UltimateTeam.Toolkit
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
            var loginResponse = await loginRequest.PerformRequestAsync();
            RequestFactories.PhishingToken = loginResponse.PhishingToken;
            RequestFactories.SessionId = loginResponse.SessionId;
            RequestFactories.NucleusId = loginResponse.NucleusId;
            RequestFactories.PersonaId = loginResponse.PersonaId;

            return loginResponse;
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

        public Task<Item> GetItemAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return RequestFactories.ItemRequestFactory(auctionInfo.CalculateBaseId()).PerformRequestAsync();
        }

        public Task<Item> GetItemAsync(long resourceId)
        {
            if (resourceId < 1) throw new ArgumentException("Definitely not valid", nameof(resourceId));

            return RequestFactories.ItemRequestFactory(resourceId.CalculateBaseId()).PerformRequestAsync();
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

        public Task<PileSizeResponse> GetPileSizeAsync()
        {
            return RequestFactories.PileSizeRequestFactory().PerformRequestAsync();
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

        public Task AddToWatchlistRequestAsync(IEnumerable<AuctionInfo> auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return RequestFactories.AddToWatchlistRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task AddToWatchlistRequestAsync(AuctionInfo auctionInfo)
        {
            return AddToWatchlistRequestAsync(new[] { auctionInfo });
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

        public Task<SendItemToClubResponse> SendItemToClubAsync(ItemData itemData)
        {
            itemData.ThrowIfNullArgument();

            return RequestFactories.SendItemToClubRequestFactory(itemData).PerformRequestAsync();
        }

        public Task<SendItemToTradePileResponse> SendItemToTradePileAsync(ItemData itemData)
        {
            itemData.ThrowIfNullArgument();

            return RequestFactories.SendItemToTradePileRequestFactory(itemData).PerformRequestAsync();
        }

        public Task<QuickSellResponse> QuickSellItemAsync(long itemId)
        {
            if (itemId < 1) throw new ArgumentException("Definitely not valid", nameof(itemId));

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

        public Task<byte[]> GetNationImageAsync(Item item)
        {
            item.ThrowIfNullArgument();

            return RequestFactories.NationImageRequestFactory(item).PerformRequestAsync();
        }

        public Task<RelistResponse> ReListAsync()
        {
            return RequestFactories.ReListRequestFactory().PerformRequestAsync();
        }

        public Task<ListGiftsResponse> GetGiftsListAsync()
        {
            return RequestFactories.GiftListRequestFactory().PerformRequestAsync();
        }

        public Task GetGiftAsync(int idGift)
        {
            return RequestFactories.GiftRequestFactory(idGift).PerformRequestAsync();
        }

        public Task<List<PriceRange>> GetPriceRangesAsync(IEnumerable<long> itemIds)
        {
            return RequestFactories.GetPriceRangesFactory(itemIds).PerformRequestAsync();
        }

        public Task<CaptchaResponse> GetCaptchaAsync()
        {
            return RequestFactories.GetCaptchaFactory().PerformRequestAsync();
        }

        public Task<byte> ValidateCaptchaAsync(int answer)
        {
            return RequestFactories.ValidateCaptchaFactory(answer).PerformRequestAsync();
        }

        public Task RemoveSoldItemsFromTradePileAsync()
        {
            return RequestFactories.RemoveSoldItemsFromTradePileRequestFactory().PerformRequestAsync();
        }

        public Task<StoreResponse> GetPackDetailsAsync()
        {
            return RequestFactories.GetPackDetailsFactory().PerformRequestAsync();
        }

        public Task<PurchasedPackResponse> BuyPackAsync(PackDetails packDetails)
        {
            return RequestFactories.BuyPackRequestFactory(packDetails).PerformRequestAsync();
        }
    }
}
