using System;
using System.Collections.Generic;
using System.Net;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Requests;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Factories
{
    public class FutRequestFactories
    {
        private readonly CookieContainer _cookieContainer = new CookieContainer();

        private string _phishingToken;

        private string _sessionId;

        public string PhishingToken
        {
            get { return _phishingToken; }
            set
            {
                value.ThrowIfInvalidArgument();
                _phishingToken = value;
            }
        }

        public string SessionId
        {
            get { return _sessionId; }
            set
            {
                value.ThrowIfInvalidArgument();
                _sessionId = value;
            }
        }

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

        private Func<AuctionInfo, IFutRequest<byte>> _removeFromTradePileRequestFactory;
        
        private Func<ItemData, IFutRequest<TradePileResponse>> _sendItemToTradePileRequestFactory;

        private Func<long, IFutRequest<QuickSellResponse>> _quickSellRequestFactory;

        public Func<LoginDetails, IFutRequest<LoginResponse>> LoginRequestFactory
        {
            get
            {
                return _loginRequestFactory ?? (_loginRequestFactory = details =>
                    {
                        var loginRequest = new LoginRequest(details);
                        loginRequest.MessageHandler.CookieContainer = _cookieContainer;
                        return loginRequest;
                    });
            }
            set
            {
                value.ThrowIfNullArgument();
                _loginRequestFactory = value;
            }
        }

        public Func<SearchParameters, IFutRequest<AuctionResponse>> SearchRequestFactory
        {
            get
            {
                return _searchRequestFactory ?? (_searchRequestFactory = parameters => new SearchRequest(parameters)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _searchRequestFactory = value;
            }
        }

        public Func<AuctionInfo, uint, IFutRequest<AuctionResponse>> PlaceBidRequestFactory
        {
            get
            {
                return _placeBidRequestFactory ?? (_placeBidRequestFactory = (info, amount) => new PlaceBidRequest(info, amount)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _placeBidRequestFactory = value;
            }
        }

        public Func<AuctionInfo, IFutRequest<Item>> ItemRequestFactory
        {
            get
            {
                return _itemRequestFactory ?? (_itemRequestFactory = info => new ItemRequest(info)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _itemRequestFactory = value;
            }
        }

        public Func<AuctionInfo, IFutRequest<byte[]>> PlayerImageRequestFactory
        {
            get
            {
                return _playerImageRequestFactory ?? (_playerImageRequestFactory = info => new PlayerImageRequest(info)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _playerImageRequestFactory = value;
            }
        }

        public Func<IEnumerable<long>, IFutRequest<AuctionResponse>> TradeStatusRequestFactory
        {
            get
            {
                return _tradeStatusRequestFactory ?? (_tradeStatusRequestFactory = tradeIds => new TradeStatusRequest(tradeIds)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _tradeStatusRequestFactory = value;
            }
        }

        public Func<IFutRequest<CreditsResponse>> CreditsRequestFactory
        {
            get
            {
                return _creditsRequestFactory ?? (_creditsRequestFactory = () => new CreditsRequest
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _creditsRequestFactory = value;
            }
        }

        public Func<IFutRequest<TradePileResponse>> TradePileRequestFactory
        {
            get
            {
                return _tradePileRequestFactory ?? (_tradePileRequestFactory = () => new TradePileRequest
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _tradePileRequestFactory = value;
            }
        }

        public Func<IFutRequest<WatchlistResponse>> WatchlistRequestFactory
        {
            get
            {
                return _watchlistRequestFactory ?? (_watchlistRequestFactory = () => new WatchlistRequest
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _watchlistRequestFactory = value;
            }
        }

        public Func<IFutRequest<PurchasedItemsResponse>> PurchasedItemsRequestFactory
        {
            get
            {
                return _purchaseditemsRequestFactory ?? (_purchaseditemsRequestFactory = () => new PurchasedItemsRequest
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _purchaseditemsRequestFactory = value;
            }
        }

        public Func<AuctionDetails, IFutRequest<ListAuctionResponse>> ListAuctionFactory
        {
            get
            {
                return _listAuctionRequestFactory ?? (_listAuctionRequestFactory = details => new ListAuctionRequest(details)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _listAuctionRequestFactory = value;
            }
        }

        public Func<AuctionInfo, IFutRequest<byte>> RemoveFromWatchlistRequestFactory
        {
            get
            {
                return _removeFromWatchlistRequestFactory ?? (_removeFromWatchlistRequestFactory = info => new RemoveFromWatchlistRequest(info)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _removeFromWatchlistRequestFactory = value;
            }
        }

        public Func<AuctionInfo, IFutRequest<byte>> RemoveFromTradePileRequestFactory
        {
            get
            {
                return _removeFromTradePileRequestFactory ?? (_removeFromTradePileRequestFactory = info => new RemoveFromTradePileRequest(info)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _removeFromTradePileRequestFactory = value;
            }
        }


        public Func<ItemData, IFutRequest<TradePileResponse>> SendItemToTradePileRequestFactory
        {
            get
            {
                return _sendItemToTradePileRequestFactory ?? (_sendItemToTradePileRequestFactory = itemData => new SendItemToTradePileRequest(itemData)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _sendItemToTradePileRequestFactory = value;
            }
        }

        public Func<long, IFutRequest<QuickSellResponse>> QuickSellRequestFactory
        {
            get
            {
                return _quickSellRequestFactory ?? (_quickSellRequestFactory = itemId => new QuickSellRequest(itemId)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _quickSellRequestFactory = value;
            }
        }
    }
}