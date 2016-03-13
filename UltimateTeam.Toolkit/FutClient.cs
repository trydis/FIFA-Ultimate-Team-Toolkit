using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Factories;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Services;

namespace UltimateTeam.Toolkit
{
    public class FutClient : IFutClient
    {
        private readonly FutRequestFactories _requestFactories;

        public FutRequestFactories RequestFactories { get { return _requestFactories; } }

        public FutClient()
        {
            _requestFactories = new FutRequestFactories();
        }

        public FutClient(CookieContainer cookieContainer)
        {
            _requestFactories = new FutRequestFactories(cookieContainer);
        }

        public async Task<LoginResponse> LoginAsync(LoginDetails loginDetails, ITwoFactorCodeProvider twoFactorCodeProvider)
        {
            loginDetails.ThrowIfNullArgument();
            _requestFactories.AppVersion.ThrowIfNullArgument();

            if (_requestFactories.AppVersion == AppVersion.WebApp)
            {
                var loginRequest = _requestFactories.LoginRequestFactory(loginDetails, twoFactorCodeProvider);
                var loginResponse = await loginRequest.PerformRequestAsync(_requestFactories.AppVersion);
                RequestFactories.PhishingToken = loginResponse.PhishingToken;
                RequestFactories.SessionId = loginResponse.SessionId;
                RequestFactories.NucleusId = loginResponse.NucleusId;
                RequestFactories.PersonaId = loginResponse.PersonaId;

                return loginResponse;
            }
            else if (_requestFactories.AppVersion == AppVersion.CompanionApp)
            {
                var loginRequest = _requestFactories.LoginRequestFactory(loginDetails, twoFactorCodeProvider);
                var loginResponse = await loginRequest.PerformRequestAsync(_requestFactories.AppVersion);

                RequestFactories.PhishingToken = loginResponse.PhishingToken;
                RequestFactories.SessionId = loginResponse.SessionId;
                RequestFactories.NucleusId = loginResponse.NucleusId;
                RequestFactories.PersonaId = loginResponse.PersonaId;

                return loginResponse;
            }
            else
            {
                return null;
            }
        }

        public async Task<AuctionResponse> SearchAsync(SearchParameters searchParameters)
        {
            searchParameters.ThrowIfNullArgument();
            _requestFactories.AppVersion.ThrowIfNullArgument();

            if (_requestFactories.AppVersion == AppVersion.WebApp)
            {
                var searchResponse = await _requestFactories.SearchRequestFactory(searchParameters).PerformRequestAsync(_requestFactories.AppVersion);
                return searchResponse;
            }
            else if (_requestFactories.AppVersion == AppVersion.CompanionApp)
            {
                var searchResponse = await _requestFactories.SearchRequestFactory(searchParameters).PerformRequestAsync(_requestFactories.AppVersion);
                return searchResponse;
            }
            else
            {
                return null;
            }
        }

        public Task<AuctionResponse> PlaceBidAsync(AuctionInfo auctionInfo, uint bidAmount = 0)
        {
            auctionInfo.ThrowIfNullArgument();

            if (bidAmount == 0)
            {
                bidAmount = auctionInfo.CalculateBid();
            }

            return _requestFactories.PlaceBidRequestFactory(auctionInfo, bidAmount).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<Item> GetItemAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.ItemRequestFactory(auctionInfo.CalculateBaseId()).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<Item> GetItemAsync(long resourceId)
        {
            if (resourceId < 1) throw new ArgumentException("Definitely not valid", "resourceId");

            return _requestFactories.ItemRequestFactory(resourceId.CalculateBaseId()).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<byte[]> GetPlayerImageAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.PlayerImageRequestFactory(auctionInfo).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<AuctionResponse> GetTradeStatusAsync(IEnumerable<long> tradeIds)
        {
            tradeIds.ThrowIfNullArgument();

            return _requestFactories.TradeStatusRequestFactory(tradeIds).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<CreditsResponse> GetCreditsAsync()
        {
            return _requestFactories.CreditsRequestFactory().PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<PileSizeResponse> GetPileSizeAsync()
        {
            return _requestFactories.PileSizeRequestFactory().PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<ConsumablesResponse> GetConsumablesAsync()
        {
            return _requestFactories.ConsumablesRequestFactory().PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<AuctionResponse> GetTradePileAsync()
        {
            return _requestFactories.TradePileRequestFactory().PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<WatchlistResponse> GetWatchlistAsync()
        {
            return _requestFactories.WatchlistRequestFactory().PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<ClubItemResponse> GetClubItemsAsync()
        {
            return _requestFactories.ClubItemRequestFactory().PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<SquadListResponse> GetSquadListAsync()
        {
            return _requestFactories.SquadListRequestFactory().PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<PurchasedItemsResponse> GetPurchasedItemsAsync()
        {
            return _requestFactories.PurchasedItemsRequestFactory().PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<ListAuctionResponse> ListAuctionAsync(AuctionDetails auctionDetails)
        {
            auctionDetails.ThrowIfNullArgument();

            return _requestFactories.ListAuctionFactory(auctionDetails).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task AddToWatchlistRequestAsync(IEnumerable<AuctionInfo> auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.AddToWatchlistRequestFactory(auctionInfo).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task AddToWatchlistRequestAsync(AuctionInfo auctionInfo)
        {
            return AddToWatchlistRequestAsync(new[] { auctionInfo });
        }

        public Task RemoveFromWatchlistAsync(IEnumerable<AuctionInfo> auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.RemoveFromWatchlistRequestFactory(auctionInfo).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task RemoveFromWatchlistAsync(AuctionInfo auctionInfo)
        {
            return RemoveFromWatchlistAsync(new[] { auctionInfo });
        }

        public Task RemoveFromTradePileAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.RemoveFromTradePileRequestFactory(auctionInfo).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<SquadDetailsResponse> GetSquadDetailsAsync(ushort squadId)
        {
            return _requestFactories.SquadDetailsRequestFactory(squadId).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<SendItemToClubResponse> SendItemToClubAsync(ItemData itemData)
        {
            itemData.ThrowIfNullArgument();

            return _requestFactories.SendItemToClubRequestFactory(itemData).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<SendItemToTradePileResponse> SendItemToTradePileAsync(ItemData itemData)
        {
            itemData.ThrowIfNullArgument();

            return _requestFactories.SendItemToTradePileRequestFactory(itemData).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<QuickSellResponse> QuickSellItemAsync(long itemId)
        {
            if (itemId < 1) throw new ArgumentException("Definitely not valid", "itemId");

            return QuickSellItemAsync(new[] { itemId });
        }

        public Task<QuickSellResponse> QuickSellItemAsync(IEnumerable<long> itemIds)
        {
            if (itemIds == null) throw new ArgumentNullException("itemIds");

            foreach (var itemId in itemIds)
                if (itemId < 1)
                    throw new ArgumentException(string.Format("ItemId {0} is definitely not valid", itemId), "itemId");

            return _requestFactories.QuickSellRequestFactory(itemIds).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<byte[]> GetClubImageAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.ClubImageRequestFactory(auctionInfo).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<DefinitionResponse> GetDefinitionsAsync(long baseId)
        {
            baseId.ThrowIfNullArgument();

            return _requestFactories.DefinitionRequestFactory(baseId).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<byte[]> GetNationImageAsync(Item item)
        {
            item.ThrowIfNullArgument();

            return _requestFactories.NationImageRequestFactory(item).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task ReListAsync()
        {
            return _requestFactories.ReListRequestFactory().PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<ListGiftsResponse> GetGiftsListAsync()
        {
            return _requestFactories.GiftListRequestFactory().PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task GetGiftAsync(int idGift)
        {
            return _requestFactories.GiftRequestFactory(idGift).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<List<PriceRange>> GetPriceRangesAsync(IEnumerable<long> itemIds)
        {
            return _requestFactories.GetPriceRangesFactory(itemIds).PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<CaptchaResponse> GetCaptchaAsync()
        {
            return _requestFactories.GetCaptchaFactory().PerformRequestAsync(_requestFactories.AppVersion);
        }

        public Task<byte> ValidateCaptchaAsync(int answer)
        {
            return _requestFactories.ValidateCaptchaFactory(answer).PerformRequestAsync(_requestFactories.AppVersion);
        }
    }
}
