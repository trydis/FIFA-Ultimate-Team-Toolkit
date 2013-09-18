using System;
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
        private readonly IFutRequestFactories _requestFactories;

        public FutClient()
            : this(new FutRequestFactories())
        {
        }

        public FutClient(IFutRequestFactories requestFactories)
        {
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
    }
}
