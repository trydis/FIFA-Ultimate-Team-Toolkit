using System;
using System.Collections.Generic;
using System.Net;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Requests;

namespace UltimateTeam.Toolkit.Factories
{
    public class FutRequestFactories
    {
        private readonly CookieContainer _cookieContainer = new CookieContainer();

        private readonly Resources _resources = new Resources();

        private string _phishingToken;

        private string _sessionId;

        private IHttpClient _httpClient;

        private Func<LoginDetails, IFutRequest<LoginResponse>> _loginRequestFactory;

        private Func<SearchParameters, IFutRequest<AuctionResponse>> _searchRequestFactory;

        private Func<AuctionInfo, uint, IFutRequest<AuctionResponse>> _placeBidRequestFactory;

        private Func<long, IFutRequest<Item>> _itemRequestFactory;

        private Func<AuctionInfo, IFutRequest<byte[]>> _playerImageRequestFactory;

        private Func<AuctionInfo, IFutRequest<byte[]>> _clubImageRequestFactory;

        private Func<Item, IFutRequest<byte[]>> _nationImageRequestFactory;

        private Func<IEnumerable<long>, IFutRequest<AuctionResponse>> _tradeStatusRequestFactory;

        private Func<IFutRequest<CreditsResponse>> _creditsRequestFactory;

        private Func<IFutRequest<AuctionResponse>> _tradePileRequestFactory;

        private Func<IFutRequest<WatchlistResponse>> _watchlistRequestFactory;

        private Func<IFutRequest<ClubItemResponse>> _clubItemRequestFactory;

        private Func<IFutRequest<SquadListResponse>> _squadListRequestFactory;

        private Func<IFutRequest<PurchasedItemsResponse>> _purchaseditemsRequestFactory;

        private Func<AuctionDetails, IFutRequest<ListAuctionResponse>> _listAuctionRequestFactory;

        private Func<IEnumerable<AuctionInfo>, IFutRequest<byte>> _addToWatchlistRequestFactory;

        private Func<IEnumerable<AuctionInfo>, IFutRequest<byte>> _removeFromWatchlistRequestFactory;

        private Func<AuctionInfo, IFutRequest<byte>> _removeFromTradePileRequestFactory;

        private Func<ushort, IFutRequest<SquadDetailsResponse>> _squadDetailsRequestFactory;

        private Func<ItemData, IFutRequest<SendItemToTradePileResponse>> _sendItemToTradePileRequestFactory;

        private Func<ItemData, IFutRequest<SendItemToClubResponse>> _sendItemToClubRequestFactory;

        private Func<IEnumerable<long>, IFutRequest<QuickSellResponse>> _quickSellRequestFactory;

        private Func<IFutRequest<PileSizeResponse>> _pileSizeRequestFactory;

        private Func<IFutRequest<ConsumablesResponse>> _consumablesRequestFactory;

        private Func<IFutRequest<byte>> _reListRequestFactory;

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

        internal IHttpClient HttpClient
        {
            get
            {
                var httpClient = _httpClient ?? (_httpClient = new HttpClientWrapper());
                httpClient.ClearRequestHeaders();
                return httpClient;
            }
            set
            {
                value.ThrowIfNullArgument();
                _httpClient = value;
            }
        }

        public Func<LoginDetails, IFutRequest<LoginResponse>> LoginRequestFactory
        {
            get
            {
                return _loginRequestFactory ?? (_loginRequestFactory = details =>
                    {
                        if (details.Platform == Platform.Xbox360)
                        {
                            _resources.FutHome = Resources.FutHomeXbox360;
                        }
                        var loginRequest = new LoginRequest(details) { HttpClient = HttpClient, Resources = _resources };
                        loginRequest.SetCookieContainer(_cookieContainer);
                        return loginRequest;
                    });
            }
            set
            {
                value.ThrowIfNullArgument();
                _loginRequestFactory = value;
            }
        }

        private T SetSharedRequestProperties<T>(T request) where T : FutRequestBase
        {
            request.PhishingToken = PhishingToken;
            request.SessionId = SessionId;
            request.HttpClient = HttpClient;
            request.Resources = _resources;

            return request;
        }

        public Func<SearchParameters, IFutRequest<AuctionResponse>> SearchRequestFactory
        {
            get
            {
                return _searchRequestFactory ??
                       (_searchRequestFactory = parameters => SetSharedRequestProperties(new SearchRequest(parameters)));
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
                return _placeBidRequestFactory ??
                       (_placeBidRequestFactory =
                           (info, amount) => SetSharedRequestProperties(new PlaceBidRequest(info, amount)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _placeBidRequestFactory = value;
            }
        }

        public Func<long, IFutRequest<Item>> ItemRequestFactory
        {
            get
            {
                return _itemRequestFactory ??
                       (_itemRequestFactory = baseId => SetSharedRequestProperties(new ItemRequest(baseId)));
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
                return _playerImageRequestFactory ??
                       (_playerImageRequestFactory = info => SetSharedRequestProperties(new PlayerImageRequest(info)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _playerImageRequestFactory = value;
            }
        }

        public Func<AuctionInfo, IFutRequest<byte[]>> ClubImageRequestFactory
        {
            get
            {
                return _clubImageRequestFactory ??
                       (_clubImageRequestFactory = info => SetSharedRequestProperties(new ClubImageRequest(info)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _clubImageRequestFactory = value;
            }
        }

        public Func<Item, IFutRequest<byte[]>> NationImageRequestFactory
        {
            get
            {
                return _nationImageRequestFactory ??
                       (_nationImageRequestFactory = info => SetSharedRequestProperties(new NationImageRequest(info)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _nationImageRequestFactory = value;
            }
        }

        public Func<IEnumerable<long>, IFutRequest<AuctionResponse>> TradeStatusRequestFactory
        {
            get
            {
                return _tradeStatusRequestFactory ??
                       (_tradeStatusRequestFactory = tradeIds => SetSharedRequestProperties(new TradeStatusRequest(tradeIds)));
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
                return _creditsRequestFactory ??
                       (_creditsRequestFactory = () => SetSharedRequestProperties(new CreditsRequest()));
            }
            set
            {
                value.ThrowIfNullArgument();
                _creditsRequestFactory = value;
            }
        }

        public Func<IFutRequest<AuctionResponse>> TradePileRequestFactory
        {
            get
            {
                return _tradePileRequestFactory ??
                       (_tradePileRequestFactory = () => SetSharedRequestProperties(new TradePileRequest()));
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
                return _watchlistRequestFactory ??
                       (_watchlistRequestFactory = () => SetSharedRequestProperties(new WatchlistRequest()));
            }
            set
            {
                value.ThrowIfNullArgument();
                _watchlistRequestFactory = value;
            }
        }

        public Func<IFutRequest<ClubItemResponse>> ClubItemRequestFactory
        {
            get
            {
                return _clubItemRequestFactory ??
                       (_clubItemRequestFactory = () => SetSharedRequestProperties(new ClubItemRequest()));
            }
            set
            {
                value.ThrowIfNullArgument();
                _clubItemRequestFactory = value;
            }
        }

        public Func<IFutRequest<SquadListResponse>> SquadListRequestFactory
        {
            get
            {
                return _squadListRequestFactory ??
                       (_squadListRequestFactory = () => SetSharedRequestProperties(new SquadListRequest()));
            }
            set
            {
                value.ThrowIfNullArgument();
                _squadListRequestFactory = value;
            }
        }

        public Func<IFutRequest<PurchasedItemsResponse>> PurchasedItemsRequestFactory
        {
            get
            {
                return _purchaseditemsRequestFactory ??
                       (_purchaseditemsRequestFactory = () => SetSharedRequestProperties(new PurchasedItemsRequest()));
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
                return _listAuctionRequestFactory ??
                       (_listAuctionRequestFactory = details => SetSharedRequestProperties(new ListAuctionRequest(details)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _listAuctionRequestFactory = value;
            }
        }

        public Func<IEnumerable<AuctionInfo>, IFutRequest<byte>> AddToWatchlistRequestFactory
        {
            get
            {
                return _addToWatchlistRequestFactory ??
                       (_addToWatchlistRequestFactory = info => SetSharedRequestProperties(new AddToWatchlistRequest(info)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _addToWatchlistRequestFactory = value;
            }
        }


        public Func<IEnumerable<AuctionInfo>, IFutRequest<byte>> RemoveFromWatchlistRequestFactory
        {
            get
            {
                return _removeFromWatchlistRequestFactory ??
                       (_removeFromWatchlistRequestFactory =
                           info => SetSharedRequestProperties(new RemoveFromWatchlistRequest(info)));
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
                return _removeFromTradePileRequestFactory ??
                       (_removeFromTradePileRequestFactory =
                           info => SetSharedRequestProperties(new RemoveFromTradePileRequest(info)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _removeFromTradePileRequestFactory = value;
            }
        }

        public Func<ushort, IFutRequest<SquadDetailsResponse>> SquadDetailsRequestFactory
        {
            get
            {
                return _squadDetailsRequestFactory ??
                       (_squadDetailsRequestFactory = squadId => SetSharedRequestProperties(new SquadDetailsRequest(squadId)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _squadDetailsRequestFactory = value;
            }
        }

        public Func<ItemData, IFutRequest<SendItemToClubResponse>> SendItemToClubRequestFactory
        {
            get
            {
                return _sendItemToClubRequestFactory ??
                       (_sendItemToClubRequestFactory =
                           itemData => SetSharedRequestProperties(new SendItemToClubRequest(itemData)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _sendItemToClubRequestFactory = value;
            }
        }

        public Func<ItemData, IFutRequest<SendItemToTradePileResponse>> SendItemToTradePileRequestFactory
        {
            get
            {
                return _sendItemToTradePileRequestFactory ??
                       (_sendItemToTradePileRequestFactory =
                           itemData => SetSharedRequestProperties(new SendItemToTradePileRequest(itemData)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _sendItemToTradePileRequestFactory = value;
            }
        }

        public Func<IEnumerable<long>, IFutRequest<QuickSellResponse>> QuickSellRequestFactory
        {
            get
            {
                return _quickSellRequestFactory ??
                       (_quickSellRequestFactory = itemId => SetSharedRequestProperties(new QuickSellRequest(itemId)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _quickSellRequestFactory = value;
            }
        }

        public Func<IFutRequest<PileSizeResponse>> PileSizeRequestFactory
        {
            get
            {
                return _pileSizeRequestFactory ??
                       (_pileSizeRequestFactory = () => SetSharedRequestProperties(new PileSizeRequest()));
            }
            set
            {
                value.ThrowIfNullArgument();
                _pileSizeRequestFactory = value;
            }
        }

        public Func<IFutRequest<ConsumablesResponse>> ConsumablesRequestFactory
        {
            get
            {
                return _consumablesRequestFactory ??
                       (_consumablesRequestFactory = () => SetSharedRequestProperties(new ConsumablesRequest()));
            }
            set
            {
                value.ThrowIfNullArgument();
                _consumablesRequestFactory = value;
            }
        }

        public Func<IFutRequest<byte>> ReListRequestFactory
        {
            get
            {
                return _reListRequestFactory ?? (_reListRequestFactory = () => SetSharedRequestProperties(new ReListRequest()));
            }
            set
            {
                value.ThrowIfNullArgument();
                _reListRequestFactory = value;
            }
        }
    }
}