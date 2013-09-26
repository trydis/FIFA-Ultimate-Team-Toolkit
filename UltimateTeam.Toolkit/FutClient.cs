using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Factories;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;

namespace UltimateTeam.Toolkit
{
    public class FutClient : IFutClient
    {
        private readonly FutRequestFactories _requestFactories;

        public FutClient()
            : this(new FutRequestFactories())
        {
        }

        public FutClient(FutRequestFactories requestFactories)
        {
            requestFactories.ThrowIfNullArgument();
            _requestFactories = requestFactories;
        }

        public async Task<LoginResponse> LoginAsync(LoginDetails loginDetails)
        {
            loginDetails.ThrowIfNullArgument();

            try
            {
                return await _requestFactories.LoginRequestFactory(loginDetails).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Login failed", e);
            }
        }

        public async Task<AuctionResponse> SearchAsync(SearchParameters searchParameters)
        {
            searchParameters.ThrowIfNullArgument();

            try
            {
                return await _requestFactories.SearchRequestFactory(searchParameters).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Search failed", e);
            }
        }

        public async Task<AuctionResponse> PlaceBidAsync(AuctionInfo auctionInfo, uint bidAmount = 0)
        {
            auctionInfo.ThrowIfNullArgument();

            if (bidAmount == 0)
            {
                bidAmount = auctionInfo.CalculateBid();
            }

            try
            {
                return await _requestFactories.PlaceBidRequestFactory(auctionInfo, bidAmount).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Placing bid failed", e);
            }
        }

        public async Task<Item> GetItemAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            try
            {
                return await _requestFactories.ItemRequestFactory(auctionInfo).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Get item failed", e);
            }
        }

        public async Task<byte[]> GetPlayerImageAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            try
            {
                return await _requestFactories.PlayerImageRequestFactory(auctionInfo).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Get player image failed", e);
            }
        }

        public async Task<AuctionResponse> GetTradeStatusAsync(IEnumerable<long> tradeIds)
        {
            tradeIds.ThrowIfNullArgument();

            try
            {
                return await _requestFactories.TradeStatusRequestFactory(tradeIds).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Get trade statuses failed", e);
            }
        }

        public async Task<CreditsResponse> GetCreditsAsync()
        {
            try
            {
                return await _requestFactories.CreditsRequestFactory().PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Get credits failed", e);
            }
        }

        public async Task<TradePileResponse> GetTradePileAsync()
        {
            try
            {
                return await _requestFactories.TradePileRequestFactory().PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Get TradePile failed", e);
            }
        }

        public async Task<WatchlistResponse> GetWatchlistAsync()
        {
            try
            {
                return await _requestFactories.WatchlistRequestFactory().PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Get Watchlist failed", e);
            }
        }

        public async Task<PurchasedItemsResponse> GetPurchasedItemsAsync()
        {
            try
            {
                return await _requestFactories.PurchasedItemsRequestFactory().PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Get Purchased Items failed", e);
            }
        }

        public async Task<ListAuctionResponse> ListAuctionAsync(AuctionDetails auctionDetails)
        {
            auctionDetails.ThrowIfNullArgument();

            try
            {
                return await _requestFactories.ListAuctionFactory(auctionDetails).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("List auction failed", e);
            }
        }

        public async Task RemoveFromWatchlistAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            try
            {
                await _requestFactories.RemoveFromWatchlistRequestFactory(auctionInfo).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Remove from watch list failed", e);
            }
        }
        
        public async Task<TradePileResponse> SendItemToTradePileAsync(ItemData itemData)
        {
            itemData.ThrowIfNullArgument();

            try
            {
                return await _requestFactories.SendItemToTradePileRequestFactory(itemData).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Send to transfer market failed", e);
            }
        }

        public async Task<QuickSellResponse> QuickSellItemAsync(long itemId)
        {
            if (itemId < 1) throw new ArgumentException("Definitely not valid", "itemId");

            try
            {
                return await _requestFactories.QuickSellRequestFactory(itemId).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Quick sell item failed", e);
            }
        }
    }
}
