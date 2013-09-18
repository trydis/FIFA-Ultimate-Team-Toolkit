using System;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Requests;

namespace UltimateTeam.Toolkit.Factories
{
    internal class FutRequestFactories : IFutRequestFactories
    {
        private Func<LoginDetails, IFutRequest<LoginResponse>> _loginRequestFactory;

        public Func<LoginDetails, IFutRequest<LoginResponse>> LoginRequestFactory
        {
            get { return _loginRequestFactory ?? (_loginRequestFactory = details => new LoginRequest(details)); }
            set { _loginRequestFactory = value; }
        }
    }
}