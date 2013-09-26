using System;
using System.Collections.Generic;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Requests;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Factories
{
    public class FutRequestFactories
    {
        private Func<LoginDetails, IFutRequest<LoginResponse>> _loginRequestFactory;

        private Func<SearchParameters, IFutRequest<AuctionResponse>> _searchRequestFactory;

        private Func<AuctionInfo, uint, IFutRequest<AuctionResponse>> _placeBidRequestFactory;

        private Func<AuctionInfo, IFutRequest<Item>> _itemRequestFactory;

        private Func<AuctionInfo, IFutRequest<byte[]>> _playerImageRequestFactory;

        private Func<IEnumerable<long>, IFutRequest<AuctionResponse>> _tradeStatusRequestFactory;

        private Func<IFutRequest<CreditsResponse>> _creditsRequestFactory;
        
        private Func<IFutRequest<TradePileResponse>> _tradePileRequestFactory;

        private Func<IFutRequest<WatchlistResponse>> _watchlistRequestFactory;

        private Func<IFutRequest<PurchasedItemsResponse>> _purchaseditemsRequestFactory;

        private Func<AuctionDetails, IFutRequest<ListAuctionResponse>> _listAuctionRequestFactory;
        
        private Func<AuctionInfo, IFutRequest<byte>> _removeFromWatchlistRequestFactory;
        
        private Func<ItemData, IFutRequest<TradePileResponse>> _sendItemToTradePileRequestFactory;

        private Func<long, IFutRequest<QuickSellResponse>> _quickSellRequestFactory;

        public Func<LoginDetails, IFutRequest<LoginResponse>> LoginRequestFactory
        {
            get { return _loginRequestFactory ?? (_loginRequestFactory = details => new LoginRequest(details)); }
            set
            {
                value.ThrowIfNullArgument();
                _loginRequestFactory = value;
            }
        }

        public Func<SearchParameters, IFutRequest<AuctionResponse>> SearchRequestFactory
        {
            get { return _searchRequestFactory ?? (_searchRequestFactory = parameters => new SearchRequest(parameters)); }
            set
            {
                value.ThrowIfNullArgument();
                _searchRequestFactory = value;
            }
        }

        public Func<AuctionInfo, uint, IFutRequest<AuctionResponse>> PlaceBidRequestFactory
        {
            get { return _placeBidRequestFactory ?? (_placeBidRequestFactory = (info, amount) => new PlaceBidRequest(info, amount)); }
            set
            {
                value.ThrowIfNullArgument();
                _placeBidRequestFactory = value;
            }
        }

        public Func<AuctionInfo, IFutRequest<Item>> ItemRequestFactory
        {
            get { return _itemRequestFactory ?? (_itemRequestFactory = info => new ItemRequest(info)); }
            set
            {
                value.ThrowIfNullArgument();
                _itemRequestFactory = value;
            }
        }

        public Func<AuctionInfo, IFutRequest<byte[]>> PlayerImageRequestFactory
        {
            get { return _playerImageRequestFactory ?? (_playerImageRequestFactory = info => new PlayerImageRequest(info)); }
            set
            {
                value.ThrowIfNullArgument();
                _playerImageRequestFactory = value;
            }
        }

        public Func<IEnumerable<long>, IFutRequest<AuctionResponse>> TradeStatusRequestFactory
        {
            get { return _tradeStatusRequestFactory ?? (_tradeStatusRequestFactory = tradeIds => new TradeStatusRequest(tradeIds)); }
            set
            {
                value.ThrowIfNullArgument();
                _tradeStatusRequestFactory = value;
            }
        }

        public Func<IFutRequest<CreditsResponse>> CreditsRequestFactory
        {
            get { return _creditsRequestFactory ?? (_creditsRequestFactory = () => new CreditsRequest()); }
            set
            {
                value.ThrowIfNullArgument();
                _creditsRequestFactory = value;
            }
        }

        public Func<IFutRequest<TradePileResponse>> TradePileRequestFactory
        {
            get { return _tradePileRequestFactory ?? (_tradePileRequestFactory = () => new TradePileRequest()); }
            set
            {
                value.ThrowIfNullArgument();
                _tradePileRequestFactory = value;
            }
        }

        public Func<IFutRequest<WatchlistResponse>> WatchlistRequestFactory
        {
            get { return _watchlistRequestFactory ?? (_watchlistRequestFactory = () => new WatchlistRequest()); }
            set
            {
                value.ThrowIfNullArgument();
                _watchlistRequestFactory = value;
            }
        }

        public Func<IFutRequest<PurchasedItemsResponse>> PurchasedItemsRequestFactory
        {
            get { return _purchaseditemsRequestFactory ?? (_purchaseditemsRequestFactory = () => new PurchasedItemsRequest()); }
            set
            {
                value.ThrowIfNullArgument();
                _purchaseditemsRequestFactory = value;
            }
        }

        public Func<AuctionDetails, IFutRequest<ListAuctionResponse>> ListAuctionFactory
        {
            get { return _listAuctionRequestFactory ?? (_listAuctionRequestFactory = details => new ListAuctionRequest(details)); }
            set
            {
                value.ThrowIfNullArgument();
                _listAuctionRequestFactory = value;
            }
        }

        public Func<AuctionInfo, IFutRequest<byte>> RemoveFromWatchlistRequestFactory
        {
            get { return _removeFromWatchlistRequestFactory ?? (_removeFromWatchlistRequestFactory = info => new RemoveFromWatchlistRequest(info)); }
            set
            {
                value.ThrowIfNullArgument();
                _removeFromWatchlistRequestFactory = value;
            }
        }

        public Func<ItemData, IFutRequest<TradePileResponse>> SendItemToTradePileRequestFactory
        {
            get { return _sendItemToTradePileRequestFactory ?? (_sendItemToTradePileRequestFactory = itemData => new SendItemToTradePileRequest(itemData)); }
            set
            {
                value.ThrowIfNullArgument();
                _sendItemToTradePileRequestFactory = value;
            }
        }

        public Func<long, IFutRequest<QuickSellResponse>> QuickSellRequestFactory
        {
            get { return _quickSellRequestFactory ?? (_quickSellRequestFactory = itemId => new QuickSellRequest(itemId)); }
            set
            {
                value.ThrowIfNullArgument();
                _quickSellRequestFactory = value;
            }
        }
    }
}