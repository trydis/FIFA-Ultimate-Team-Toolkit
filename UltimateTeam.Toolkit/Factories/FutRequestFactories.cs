using System;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Requests;

namespace UltimateTeam.Toolkit.Factories
{
    internal class FutRequestFactories : IFutRequestFactories
    {
        private Func<LoginDetails, IFutRequest<LoginResponse>> _loginRequestFactory;

        private Func<SearchParameters, IFutRequest<AuctionResponse>> _searchRequestFactory;

        private Func<AuctionInfo, uint, IFutRequest<AuctionResponse>> _placeBidRequestFactory;

        public Func<LoginDetails, IFutRequest<LoginResponse>> LoginRequestFactory
        {
            get { return _loginRequestFactory ?? (_loginRequestFactory = details => new LoginRequest(details)); }
            set { _loginRequestFactory = value; }
        }

        public Func<SearchParameters, IFutRequest<AuctionResponse>> SearchRequestFactory
        {
            get { return _searchRequestFactory ?? (_searchRequestFactory = parameters => new SearchRequest(parameters)); }
            set { _searchRequestFactory = value; }
        }

        public Func<AuctionInfo, uint, IFutRequest<AuctionResponse>> PlaceBidRequestFactory
        {
            get { return _placeBidRequestFactory ?? (_placeBidRequestFactory = (info, amount) => new PlaceBidRequest(info, amount)); }
            set { _placeBidRequestFactory = value; }
        }
    }
}