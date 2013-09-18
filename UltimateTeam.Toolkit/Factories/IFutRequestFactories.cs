using System;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Requests;

namespace UltimateTeam.Toolkit.Factories
{
    public interface IFutRequestFactories
    {
        Func<LoginDetails, IFutRequest<LoginResponse>> LoginRequestFactory { get; set; }       
    }
}