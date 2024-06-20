using Newtonsoft.Json;
using System.Collections;
using System.Net;
using System.Reflection;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Factories;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Models.Auth;
using UltimateTeam.Toolkit.Models.Generic;
using UltimateTeam.Toolkit.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UltimateTeam.Toolkit.Tests.Helpers;
using UltimateTeam.Toolkit.Parameters;

namespace UltimateTeam.Toolkit.Tests
{
    [TestClass]
    public class SearchPlayerAsync : TestBase
    {
        [TestMethod]
        public async Task SearchPlayerAsync_AssertAuctionCount()
        {
            LoginDetails loginDetails = new LoginDetails("<user>", "<password>", Platform.Ps5, AppVersion.WebApp);
            LoginResponse loginResponse = await FutClient.LoginAsync(loginDetails, TwoFactorProvider);

            AuctionResponse searchResponse = new AuctionResponse();
            var searchParameters = new PlayerSearchParameters
            {
                Page = 1,
                Level = Level.Gold,
                ChemistryStyle = ChemistryStyle.All,
                League = League.Bundesliga,
                Nation = Nation.Germany,
                Position = Position.Midfielders,
                Team = Team.BorussiaDortmund,
                HasContraints = false,
                MinBid = 150,
                MaxBid = 750,
                MinBuy = 500,
                MaxBuy = 800
            };
            searchResponse = await FutClient.SearchAsync(searchParameters);

            Assert.IsTrue(searchResponse.AuctionInfo.Count > 0);
        }
    }
}