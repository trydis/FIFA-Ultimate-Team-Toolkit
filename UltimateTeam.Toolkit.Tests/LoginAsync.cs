using Microsoft.VisualStudio.TestTools.UnitTesting;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Factories;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Models.Auth;
using UltimateTeam.Toolkit.Models.Generic;
using UltimateTeam.Toolkit.Tests.Helpers;

namespace UltimateTeam.Toolkit.Tests
{
    [TestClass]
    public class LoginAsync : TestBase
    {
        [TestMethod]
        public async Task LoginAsync_AssertSid()
        {
            LoginDetails loginDetails = new LoginDetails("<user>", "<password>", Platform.Ps5, AppVersion.WebApp);
            LoginResponse loginResponse = await FutClient.LoginAsync(loginDetails, TwoFactorProvider);

            Assert.IsNotNull(loginResponse.AuthData?.Sid);
        }
    }
}