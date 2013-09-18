using System;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Requests;

namespace UltimateTeam.Toolkit.Factories
{
    public interface IFutRequestFactories
    {
        Func<LoginDetails, IFutRequest<LoginResponse>> LoginRequestFactory { get; set; }   
    
        Func<SearchParameters, IFutRequest<AuctionResponse>> SearchRequestFactory { get; set; }

        Func<AuctionInfo, uint, IFutRequest<AuctionResponse>> PlaceBidRequestFactory { get; set; }
    }
}