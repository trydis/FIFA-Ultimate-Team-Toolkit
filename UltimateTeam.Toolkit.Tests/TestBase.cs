using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using UltimateTeam.Toolkit.Factories;
using UltimateTeam.Toolkit.Services;
using UltimateTeam.Toolkit.Tests.Helpers;

namespace UltimateTeam.Toolkit.Tests
{
    public class TestBase
    {
        private FutClient _client;
        public FutClient FutClient
        {
            get { return _client; }
            set { _client = value; }
        }

        private ITwoFactorCodeProvider _provider;
        public ITwoFactorCodeProvider TwoFactorProvider
        {
            get { return _provider; }
        }

        public TestBase()
        {
            ITwoFactorCodeProvider _provider = new Auth();
            CookieContainer cookieContainer = CookieHandler.LoadCookiesFromJson("cookie.json");
            _client = new FutClient(cookieContainer);
        }
    }
}