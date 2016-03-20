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
            var loginResponse = await loginRequest.PerformRequestAsync(RequestFactories.AppVersion);
            RequestFactories.PhishingToken = loginResponse.PhishingToken;
            RequestFactories.SessionId = loginResponse.SessionId;
            RequestFactories.NucleusId = loginResponse.NucleusId;
            RequestFactories.PersonaId = loginResponse.PersonaId;

            return loginResponse;
        }

        public async Task<AuctionResponse> SearchAsync(SearchParameters searchParameters)
        {
            searchParameters.ThrowIfNullArgument();

            var searchResponse = await RequestFactories.SearchRequestFactory(searchParameters).PerformRequestAsync(RequestFactories.AppVersion);
            return searchResponse;
        }

        public Task<AuctionResponse> PlaceBidAsync(AuctionInfo auctionInfo, uint bidAmount = 0)
        {
            auctionInfo.ThrowIfNullArgument();

            if (bidAmount == 0)
            {
                bidAmount = auctionInfo.CalculateBid();
            }

            return RequestFactories.PlaceBidRequestFactory(auctionInfo, bidAmount).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<Item> GetItemAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return RequestFactories.ItemRequestFactory(auctionInfo.CalculateBaseId()).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<Item> GetItemAsync(long resourceId)
        {
            if (resourceId < 1) throw new ArgumentException("Definitely not valid", nameof(resourceId));

            return RequestFactories.ItemRequestFactory(resourceId.CalculateBaseId()).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<byte[]> GetPlayerImageAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return RequestFactories.PlayerImageRequestFactory(auctionInfo).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<AuctionResponse> GetTradeStatusAsync(IEnumerable<long> tradeIds)
        {
            tradeIds.ThrowIfNullArgument();

            return RequestFactories.TradeStatusRequestFactory(tradeIds).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<CreditsResponse> GetCreditsAsync()
        {
            return RequestFactories.CreditsRequestFactory().PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<PileSizeResponse> GetPileSizeAsync()
        {
            return RequestFactories.PileSizeRequestFactory().PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<ConsumablesResponse> GetConsumablesAsync()
        {
            return RequestFactories.ConsumablesRequestFactory().PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<AuctionResponse> GetTradePileAsync()
        {
            return RequestFactories.TradePileRequestFactory().PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<WatchlistResponse> GetWatchlistAsync()
        {
            return RequestFactories.WatchlistRequestFactory().PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<ClubItemResponse> GetClubItemsAsync()
        {
            return RequestFactories.ClubItemRequestFactory().PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<SquadListResponse> GetSquadListAsync()
        {
            return RequestFactories.SquadListRequestFactory().PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<PurchasedItemsResponse> GetPurchasedItemsAsync()
        {
            return RequestFactories.PurchasedItemsRequestFactory().PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<ListAuctionResponse> ListAuctionAsync(AuctionDetails auctionDetails)
        {
            auctionDetails.ThrowIfNullArgument();

            return RequestFactories.ListAuctionFactory(auctionDetails).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task AddToWatchlistRequestAsync(IEnumerable<AuctionInfo> auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return RequestFactories.AddToWatchlistRequestFactory(auctionInfo).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task AddToWatchlistRequestAsync(AuctionInfo auctionInfo)
        {
            return AddToWatchlistRequestAsync(new[] { auctionInfo });
        }

        public Task RemoveFromWatchlistAsync(IEnumerable<AuctionInfo> auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return RequestFactories.RemoveFromWatchlistRequestFactory(auctionInfo).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task RemoveFromWatchlistAsync(AuctionInfo auctionInfo)
        {
            return RemoveFromWatchlistAsync(new[] { auctionInfo });
        }

        public Task RemoveFromTradePileAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return RequestFactories.RemoveFromTradePileRequestFactory(auctionInfo).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<SquadDetailsResponse> GetSquadDetailsAsync(ushort squadId)
        {
            return RequestFactories.SquadDetailsRequestFactory(squadId).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<SendItemToClubResponse> SendItemToClubAsync(ItemData itemData)
        {
            itemData.ThrowIfNullArgument();

            return RequestFactories.SendItemToClubRequestFactory(itemData).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<SendItemToTradePileResponse> SendItemToTradePileAsync(ItemData itemData)
        {
            itemData.ThrowIfNullArgument();

            return RequestFactories.SendItemToTradePileRequestFactory(itemData).PerformRequestAsync(RequestFactories.AppVersion);
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

            return RequestFactories.QuickSellRequestFactory(itemIds).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<byte[]> GetClubImageAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return RequestFactories.ClubImageRequestFactory(auctionInfo).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<DefinitionResponse> GetDefinitionsAsync(long baseId)
        {
            baseId.ThrowIfNullArgument();

            return RequestFactories.DefinitionRequestFactory(baseId).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<byte[]> GetNationImageAsync(Item item)
        {
            item.ThrowIfNullArgument();

            return RequestFactories.NationImageRequestFactory(item).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task ReListAsync()
        {
            return RequestFactories.ReListRequestFactory().PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<ListGiftsResponse> GetGiftsListAsync()
        {
            return RequestFactories.GiftListRequestFactory().PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task GetGiftAsync(int idGift)
        {
            return RequestFactories.GiftRequestFactory(idGift).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<List<PriceRange>> GetPriceRangesAsync(IEnumerable<long> itemIds)
        {
            return RequestFactories.GetPriceRangesFactory(itemIds).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<CaptchaResponse> GetCaptchaAsync()
        {
            return RequestFactories.GetCaptchaFactory().PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task<byte> ValidateCaptchaAsync(int answer)
        {
            return RequestFactories.ValidateCaptchaFactory(answer).PerformRequestAsync(RequestFactories.AppVersion);
        }

        public Task RemoveSoldItemsFromTradePileAsync()
        {
            return RequestFactories.RemoveSoldItemsFromTradePileRequestFactory().PerformRequestAsync(RequestFactories.AppVersion);
        }
    }
}
