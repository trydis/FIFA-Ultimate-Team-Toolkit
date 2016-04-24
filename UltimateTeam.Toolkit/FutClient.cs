using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
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

        private LoginDetails _loginDetails;

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
            _requestFactories.AppVersion = loginDetails.AppVersion;
            _loginDetails = loginDetails;

            if (_loginDetails.AppVersion == AppVersion.WebApp)
            {
                var loginRequest = _requestFactories.LoginRequestFactory(loginDetails, twoFactorCodeProvider);
                var loginResponse = await loginRequest.PerformRequestAsync();
                if (_loginDetails.SendPinRequests == true)
                {
                    var pinResponseHome = await _requestFactories.SendPinRequestFactory(PinEventId.WebApp_Home).PerformRequestAsync();
                }

                _requestFactories.PhishingToken = loginResponse.PhishingToken;
                _requestFactories.SessionId = loginResponse.SessionId;
                _requestFactories.NucleusId = loginResponse.NucleusId;
                _requestFactories.PersonaId = loginResponse.PersonaId;


                return loginResponse;
            }
            else if (_loginDetails.AppVersion == AppVersion.CompanionApp)
            {
                var loginRequest = _requestFactories.LoginRequestFactory(loginDetails, twoFactorCodeProvider);
                var loginResponse = await loginRequest.PerformRequestAsync();
                if (_loginDetails.SendPinRequests == true)
                {
                    var pinResponsehubConnected = await _requestFactories.SendPinRequestFactory(PinEventId.CompanionApp_Connected).PerformRequestAsync();
                    var pinResponsehubHome = await _requestFactories.SendPinRequestFactory(PinEventId.CompanionApp_Home).PerformRequestAsync();
                    var pinResponseHubSquad = await _requestFactories.SendPinRequestFactory(PinEventId.CompanionApp_HubSquads).PerformRequestAsync();
                }

                _requestFactories.PhishingToken = loginResponse.PhishingToken;
                _requestFactories.SessionId = loginResponse.SessionId;
                _requestFactories.NucleusId = loginResponse.NucleusId;
                _requestFactories.PersonaId = loginResponse.PersonaId;
                _requestFactories.AppVersion = loginDetails.AppVersion;

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
                if (_loginDetails.SendPinRequests == true)
                {
                    var pinResponseMarketSearch = await _requestFactories.SendPinRequestFactory(PinEventId.Generic_TransferMarketSearch).PerformRequestAsync();
                }
                var searchResponse = await _requestFactories.SearchRequestFactory(searchParameters).PerformRequestAsync();

                if (_loginDetails.SendPinRequests == true)
                {
                    var pinResponseMarketSearchResults = await _requestFactories.SendPinRequestFactory(PinEventId.WebApp_TransferMarketSearchResults).PerformRequestAsync();
                }
                return searchResponse;
            }
            else if (_requestFactories.AppVersion == AppVersion.CompanionApp)
            {
                if (_loginDetails.SendPinRequests == true)
                {
                    var pinResponseTransferMarketSearch = await _requestFactories.SendPinRequestFactory(PinEventId.Generic_TransferMarketSearch).PerformRequestAsync();
                }
                var searchResponse = await _requestFactories.SearchRequestFactory(searchParameters).PerformRequestAsync();

                if (_loginDetails.SendPinRequests == true)
                {
                    var pinResponseTransferMarketSearchResults = await _requestFactories.SendPinRequestFactory(PinEventId.WebApp_TransferMarketSearchResults).PerformRequestAsync();
                }
                return searchResponse;
            }
            else
            {
                return null;
            }
        }

        public async Task<AuctionResponse> PlaceBidAsync(AuctionInfo auctionInfo, uint bidAmount = 0)
        {
            auctionInfo.ThrowIfNullArgument();

            if (bidAmount == 0)
            {
                bidAmount = auctionInfo.CalculateBid();
            }

            if (_requestFactories.AppVersion == AppVersion.CompanionApp)
            {
                if (_loginDetails.SendPinRequests == true)
                {
                    var pinResponseTransferlist = await _requestFactories.SendPinRequestFactory(PinEventId.CompanionApp_TransferMarketResults_Detailed).PerformRequestAsync();
                }
                var placeBidResponse = await _requestFactories.PlaceBidRequestFactory(auctionInfo, bidAmount).PerformRequestAsync();
                return placeBidResponse;
            }
            else if (_requestFactories.AppVersion == AppVersion.WebApp)
            {
                var placeBidResponse = await _requestFactories.PlaceBidRequestFactory(auctionInfo, bidAmount).PerformRequestAsync();
                return placeBidResponse;
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", _requestFactories.AppVersion.ToString()));
            }
        }

        public Task<Item> GetItemAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.ItemRequestFactory(auctionInfo.CalculateBaseId()).PerformRequestAsync();
        }

        public Task<Item> GetItemAsync(long resourceId)
        {
            if (resourceId < 1) throw new ArgumentException("Definitely not valid", nameof(resourceId));

            return _requestFactories.ItemRequestFactory(resourceId.CalculateBaseId()).PerformRequestAsync();
        }

        public Task<byte[]> GetPlayerImageAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.PlayerImageRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task<AuctionResponse> GetTradeStatusAsync(IEnumerable<long> tradeIds)
        {
            tradeIds.ThrowIfNullArgument();

            return _requestFactories.TradeStatusRequestFactory(tradeIds).PerformRequestAsync();
        }

        public Task<CreditsResponse> GetCreditsAsync()
        {
            return _requestFactories.CreditsRequestFactory().PerformRequestAsync();
        }

        public Task<PileSizeResponse> GetPileSizeAsync()
        {
            return _requestFactories.PileSizeRequestFactory().PerformRequestAsync();
        }

        public Task<ConsumablesResponse> GetConsumablesAsync()
        {
            return _requestFactories.ConsumablesRequestFactory().PerformRequestAsync();
        }

        public async Task<AuctionResponse> GetTradePileAsync()
        {
            if (_requestFactories.AppVersion == AppVersion.CompanionApp)
            {
                if (_loginDetails.SendPinRequests == true)
                {
                    var pinResponseTransferlist = await _requestFactories.SendPinRequestFactory(PinEventId.CompanionApp_TransferList).PerformRequestAsync();
                }
                var tradepileResponse = await _requestFactories.TradePileRequestFactory().PerformRequestAsync();
                return tradepileResponse;
            }
            else if (_requestFactories.AppVersion == AppVersion.WebApp)
            {
                var tradepileResponse = await _requestFactories.TradePileRequestFactory().PerformRequestAsync();
                if (_loginDetails.SendPinRequests == true)
                {
                    var pinResponseTransferlist = await _requestFactories.SendPinRequestFactory(PinEventId.WebApp_TransferList).PerformRequestAsync();
                }
                return tradepileResponse;
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", _requestFactories.AppVersion.ToString()));
            }
        }

        public async Task<WatchlistResponse> GetWatchlistAsync()
        {
            if (_requestFactories.AppVersion == AppVersion.CompanionApp)
            {
                if (_loginDetails.SendPinRequests == true)
                {
                    var pinResponseWatchlist = await _requestFactories.SendPinRequestFactory(PinEventId.CompanionApp_TransferTargets).PerformRequestAsync();
                }
                var watchlistResponse = await _requestFactories.WatchlistRequestFactory().PerformRequestAsync();
                return watchlistResponse;
            }
            else if (_requestFactories.AppVersion == AppVersion.WebApp)
            {
                var watchlistResponse = await _requestFactories.WatchlistRequestFactory().PerformRequestAsync();
                if (_loginDetails.SendPinRequests == true)
                {
                    var pinResponseTransferlist = await _requestFactories.SendPinRequestFactory(PinEventId.WebApp_TransferTargets).PerformRequestAsync();
                }
                return watchlistResponse;
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", _requestFactories.AppVersion.ToString()));
            }
        }

        public Task<ClubItemResponse> GetClubItemsAsync()
        {
            return _requestFactories.ClubItemRequestFactory().PerformRequestAsync();
        }

        public Task<SquadListResponse> GetSquadListAsync()
        {
            return _requestFactories.SquadListRequestFactory().PerformRequestAsync();
        }

        public async Task<PurchasedItemsResponse> GetPurchasedItemsAsync()
        {
            if (_requestFactories.AppVersion == AppVersion.CompanionApp)
            {
                if (_loginDetails.SendPinRequests == true)
                {
                    var pinResponseHub = await _requestFactories.SendPinRequestFactory(PinEventId.CompanionApp_HubUnassigned).PerformRequestAsync();
                    var pinResponseUnassignedItems = await _requestFactories.SendPinRequestFactory(PinEventId.CompanionApp_UnassignedItems).PerformRequestAsync();
                }
                var unassignedItemsResponse = await _requestFactories.PurchasedItemsRequestFactory().PerformRequestAsync();
                return unassignedItemsResponse;
            }
            else if (_requestFactories.AppVersion == AppVersion.WebApp)
            {
                var unassignedItemsResponse = await _requestFactories.PurchasedItemsRequestFactory().PerformRequestAsync();
                if (_loginDetails.SendPinRequests == true)
                {
                    var pinResponseUnassignedItems = await _requestFactories.SendPinRequestFactory(PinEventId.WebApp_UnassignedItems).PerformRequestAsync();
                }
                return unassignedItemsResponse;
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", _requestFactories.AppVersion.ToString()));
            }
        }

        public async Task<ListAuctionResponse> ListAuctionAsync(AuctionDetails auctionDetails)
        {
            auctionDetails.ThrowIfNullArgument();

            if (_requestFactories.AppVersion == AppVersion.CompanionApp)
            {
                if (_loginDetails.SendPinRequests == true)
                {
                    var pinResponseTransferlist = await _requestFactories.SendPinRequestFactory(PinEventId.CompanionApp_TransferList_Detailed).PerformRequestAsync();
                }
                var listAuctionResponse = await _requestFactories.ListAuctionFactory(auctionDetails).PerformRequestAsync();
                return listAuctionResponse;
            }
            else if (_requestFactories.AppVersion == AppVersion.WebApp)
            {
                var listAuctionResponse = await _requestFactories.ListAuctionFactory(auctionDetails).PerformRequestAsync();
                return listAuctionResponse;
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", _requestFactories.AppVersion.ToString()));
            }
        }

        public Task AddToWatchlistRequestAsync(IEnumerable<AuctionInfo> auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.AddToWatchlistRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task AddToWatchlistRequestAsync(AuctionInfo auctionInfo)
        {
            return AddToWatchlistRequestAsync(new[] { auctionInfo });
        }

        public Task RemoveFromWatchlistAsync(IEnumerable<AuctionInfo> auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.RemoveFromWatchlistRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task RemoveFromWatchlistAsync(AuctionInfo auctionInfo)
        {
            return RemoveFromWatchlistAsync(new[] { auctionInfo });
        }

        public Task RemoveFromTradePileAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.RemoveFromTradePileRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task<SquadDetailsResponse> GetSquadDetailsAsync(ushort squadId)
        {
            return _requestFactories.SquadDetailsRequestFactory(squadId).PerformRequestAsync();
        }

        public Task<SendItemToClubResponse> SendItemToClubAsync(ItemData itemData)
        {
            itemData.ThrowIfNullArgument();

            return _requestFactories.SendItemToClubRequestFactory(itemData).PerformRequestAsync();
        }

        public Task<SendItemToTradePileResponse> SendItemToTradePileAsync(ItemData itemData)
        {
            itemData.ThrowIfNullArgument();

            return _requestFactories.SendItemToTradePileRequestFactory(itemData).PerformRequestAsync();
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

            return _requestFactories.QuickSellRequestFactory(itemIds).PerformRequestAsync();
        }

        public Task<byte[]> GetClubImageAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.ClubImageRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task<DefinitionResponse> GetDefinitionsAsync(long baseId)
        {
            baseId.ThrowIfNullArgument();

            return _requestFactories.DefinitionRequestFactory(baseId).PerformRequestAsync();
        }

        public Task<byte[]> GetNationImageAsync(Item item)
        {
            item.ThrowIfNullArgument();

            return _requestFactories.NationImageRequestFactory(item).PerformRequestAsync();
        }

        public Task ReListAsync()
        {
            return _requestFactories.ReListRequestFactory().PerformRequestAsync();
        }

        public Task<ListGiftsResponse> GetGiftsListAsync()
        {
            return _requestFactories.GiftListRequestFactory().PerformRequestAsync();
        }

        public Task GetGiftAsync(int idGift)
        {
            return _requestFactories.GiftRequestFactory(idGift).PerformRequestAsync();
        }

        public Task<List<PriceRange>> GetPriceRangesAsync(IEnumerable<long> itemIds)
        {
            return _requestFactories.GetPriceRangesFactory(itemIds).PerformRequestAsync();
        }

        public Task<CaptchaResponse> GetCaptchaAsync()
        {
            return _requestFactories.GetCaptchaFactory().PerformRequestAsync();
        }

        public Task<byte> ValidateCaptchaAsync(int answer)
        {
            return _requestFactories.ValidateCaptchaFactory(answer).PerformRequestAsync();
        }

        public Task RemoveSoldItemsFromTradePileAsync()
        {
            return _requestFactories.RemoveSoldItemsFromTradePileRequestFactory().PerformRequestAsync();
        }

        public Task<PinResponse> SendPinRequestAsync(PinEventId pinEventId)
        {
            return _requestFactories.SendPinRequestFactory(pinEventId).PerformRequestAsync();
        }
    }
}
