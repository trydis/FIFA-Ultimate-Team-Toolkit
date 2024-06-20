using System.Net;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Models.Auction;
using UltimateTeam.Toolkit.Models.Auth;
using UltimateTeam.Toolkit.Models.Player;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.RequestFactory;
using UltimateTeam.Toolkit.Requests;
using UltimateTeam.Toolkit.Services;

namespace UltimateTeam.Toolkit.Factories
{
    public class FutRequestFactories
    {
        private readonly Resources _webResources = new Resources(AppVersion.WebApp);

        private readonly Resources _mobileResources = new Resources(AppVersion.CompanionApp);

        private Resources _resources;

        private LoginDetails _loginDetails;

        private LoginResponse _loginResponse;

        private IHttpClient _httpClient;

        private Func<LoginDetails, ITwoFactorCodeProvider, IFutRequest<LoginResponse>> _loginRequestFactory;

        private Func<SearchParameters, IFutRequest<AuctionResponse>> _searchRequestFactory;

        private Func<AuctionInfo, uint, IFutRequest<AuctionResponse>> _placeBidRequestFactory;

        private Func<AuctionInfo, IFutRequest<byte[]>> _playerImageRequestFactory;

        private Func<AuctionInfo, IFutRequest<byte[]>> _clubImageRequestFactory;

        private Func<AuctionInfo, IFutRequest<byte[]>> _nationImageRequestFactory;

        private Func<IEnumerable<long>, IFutRequest<AuctionResponse>> _tradeStatusRequestFactory;

        private Func<IFutRequest<CreditsResponse>> _creditsRequestFactory;

        private Func<IFutRequest<AuctionResponse>> _tradePileRequestFactory;

        private Func<IFutRequest<WatchlistResponse>> _watchlistRequestFactory;

        private Func<IFutRequest<ClubItemResponse>> _clubItemRequestFactory;

        private Func<IFutRequest<SquadListResponse>> _squadListRequestFactory;

        private Func<IFutRequest<PurchasedItemsResponse>> _purchaseditemsRequestFactory;

        private Func<AuctionDetails, IFutRequest<ListAuctionResponse>> _listAuctionRequestFactory;

        private Func<AuctionInfo, IFutRequest<byte>> _addToWatchlistRequestFactory;

        private Func<IEnumerable<AuctionInfo>, IFutRequest<byte>> _removeFromWatchlistRequestFactory;

        private Func<AuctionInfo, IFutRequest<byte>> _removeFromTradePileRequestFactory;

        private Func<ushort, IFutRequest<SquadDetailsResponse>> _squadDetailsRequestFactory;

        private Func<IEnumerable<long>, IFutRequest<SendToTradePileResponse>> _SendToTradePileRequestFactory;
        private Func<ItemData, AuctionInfo, IFutRequest<SendToTradePileResponse>> _sendTradeItemToTradePileRequestFactory;

        private Func<IEnumerable<long>, IFutRequest<SendToClubResponse>> _SendToClubRequestFactory;
        private Func<ItemData, AuctionInfo, IFutRequest<SendToClubResponse>> _sendTradeItemToClubRequestFactory;

        private Func<IEnumerable<long>, IFutRequest<QuickSellResponse>> _quickSellRequestFactory;

        private Func<IFutRequest<ConsumablesResponse>> _consumablesRequestFactory;

        private Func<IFutRequest<RelistResponse>> _reListRequestFactory;

        private Func<long, IFutRequest<DefinitionResponse>> _definitionRequestFactory;

        private Func<IFutRequest<byte>> _removeSoldItemsFromTradepileRequestFactory;

        private Func<IFutRequest<StoreResponse>> _getPackDetailsFactory;

        private Func<int, CurrencyOption, IFutRequest<PurchasedPackResponse>> _buyPackFactory;

        public FutRequestFactories()
        {
            CookieContainer = new CookieContainer();
        }

        public FutRequestFactories(CookieContainer cookieContainer)
        {
            CookieContainer = cookieContainer;
        }

        public CookieContainer CookieContainer { get; }

        public LoginDetails LoginDetails
        {
            get { return _loginDetails; }
            set
            {
                _loginDetails = value;
            }
        }

        public LoginResponse LoginResponse
        {
            get { return _loginResponse; }
            set
            {
                _loginResponse = value;
            }
        }

        public AppVersion AppVersion
        {
            get { return _loginDetails.AppVersion; }
            set
            {
                _loginDetails.AppVersion = value;
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

        public Func<LoginDetails, ITwoFactorCodeProvider, IFutRequest<LoginResponse>> LoginRequestFactory
        {
            get
            {
                return _loginRequestFactory ?? (_loginRequestFactory = (details, twoFactorCodeProvider) =>
                {
                    _loginDetails = details;

                    if (_loginDetails.AppVersion == AppVersion.WebApp)
                    {
                        _resources = _webResources;
                    }
                    else
                    {
                        _resources = _mobileResources;
                    }

                    var loginRequest = new LoginRequest(_loginDetails, twoFactorCodeProvider) { HttpClient = HttpClient, Resources = _resources };
                    loginRequest.SetCookieContainer(CookieContainer);
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
            request.LoginResponse = _loginResponse;
            request.LoginDetails = _loginDetails;
            request.HttpClient = _httpClient;
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

        public Func<AuctionInfo, IFutRequest<byte[]>> NationImageRequestFactory
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

        public Func<AuctionInfo, IFutRequest<byte>> AddToWatchlistRequestFactory
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

        public Func<IEnumerable<long>, IFutRequest<SendToClubResponse>> SendToClubRequestFactory
        {
            get
            {
                return _SendToClubRequestFactory ??
                       (_SendToClubRequestFactory =
                           itemIds => SetSharedRequestProperties(new SendToClubRequest(itemIds)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _SendToClubRequestFactory = value;
            }
        }

        public Func<ItemData, AuctionInfo, IFutRequest<SendToClubResponse>> SendTradeItemToClubRequestFactory
        {
            get
            {
                return _sendTradeItemToClubRequestFactory ??
                       (_sendTradeItemToClubRequestFactory = (itemData, auctionInfo) =>
                           SetSharedRequestProperties(new SendToClubRequest(itemData, auctionInfo)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _sendTradeItemToClubRequestFactory = value;
            }
        }

        public Func<IEnumerable<long>, IFutRequest<SendToTradePileResponse>> SendToTradePileRequestFactory
        {
            get
            {
                return _SendToTradePileRequestFactory ??
                       (_SendToTradePileRequestFactory =
                           itemIds => SetSharedRequestProperties(new SendToTradePileRequest(itemIds)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _SendToTradePileRequestFactory = value;
            }
        }

        public Func<ItemData, AuctionInfo, IFutRequest<SendToTradePileResponse>> SendTradeItemToTradePileRequestFactory
        {
            get
            {
                return _sendTradeItemToTradePileRequestFactory ??
                       (_sendTradeItemToTradePileRequestFactory = (itemData, auctionInfo) =>
                           SetSharedRequestProperties(new SendToTradePileRequest(itemData, auctionInfo)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _sendTradeItemToTradePileRequestFactory = value;
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

        public Func<long, IFutRequest<DefinitionResponse>> DefinitionRequestFactory
        {
            get
            {
                return _definitionRequestFactory ??
                       (_definitionRequestFactory = baseId => SetSharedRequestProperties(new DefinitionRequest(baseId)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _definitionRequestFactory = value;
            }
        }

        public Func<IFutRequest<RelistResponse>> ReListRequestFactory
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

        public Func<IFutRequest<byte>> RemoveSoldItemsFromTradePileRequestFactory
        {
            get
            {
                return _removeSoldItemsFromTradepileRequestFactory ??
                       (_removeSoldItemsFromTradepileRequestFactory = () => SetSharedRequestProperties(new RemoveSoldItemsFromTradePileRequest()));
            }
            set
            {
                value.ThrowIfNullArgument();
                _removeSoldItemsFromTradepileRequestFactory = value;
            }
        }

        public Func<IFutRequest<StoreResponse>> GetPackDetailsFactory
        {
            get
            {
                return _getPackDetailsFactory ??
                       (_getPackDetailsFactory = () => SetSharedRequestProperties(new StoreRequest()));
            }
            set
            {
                value.ThrowIfNullArgument();
                _getPackDetailsFactory = value;
            }
        }

        public Func<int, CurrencyOption, IFutRequest<PurchasedPackResponse>> BuyPackRequestFactory
        {
            get
            {
                return _buyPackFactory ??
                       (_buyPackFactory = (packId, currencyOption) => SetSharedRequestProperties(new PurchasePackRequest(packId, currencyOption)));
            }
            set
            {
                value.ThrowIfNullArgument();
                _buyPackFactory = value;
            }
        }
    }
}