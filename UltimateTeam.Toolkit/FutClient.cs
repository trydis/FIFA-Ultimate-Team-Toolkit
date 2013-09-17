using System;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Factories;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit
{
    public class FutClient : IFutClient
    {
        private readonly IFutRequestFactory _requestFactory;

        public FutClient()
        {
            _requestFactory = new FutRequestFactory();
        }

        public FutClient(IFutRequestFactory requestFactory)
        {
            _requestFactory = requestFactory;
        }

        public async Task LoginAsync(LoginDetails loginDetails)
        {
            loginDetails.ThrowIfNullArgument();

            try
            {
                await _requestFactory.CreateLoginRequest(loginDetails);
            }
            catch (Exception e)
            {
                throw new FutException("Login failed", e);
            }
        }
    }
}
