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

        private Func<AuctionDetails, IFutRequest<ListAuctionResponse>> _listAuctionRequestFactory;

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

        public Func<AuctionDetails, IFutRequest<ListAuctionResponse>> ListAuctionFactory
        {
            get { return _listAuctionRequestFactory ?? (_listAuctionRequestFactory = details => new ListAuctionRequest(details)); }
            set
            {
                value.ThrowIfNullArgument();
                _listAuctionRequestFactory = value;
            }
        }
    }
}