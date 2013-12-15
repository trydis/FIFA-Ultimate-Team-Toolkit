using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using NUnit.Framework;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Requests;

namespace UltimateTeam.Toolkit.Tests
{
    public class PerformanceTests
    {
        private IFutClient _futClient;
        private readonly Resources _resources = new Resources();

        [SetUp]
        public void Setup()
        {
            _futClient = new FutClient();
        }
        
        /// <summary>
        /// Tests how many searches can be done per second, excluding any server communication.
        /// </summary>
        [Test]
        public async void SearchAsync_PerformanceTest()
        {
            const int numberOfAuctions = 50;
            const int iterations = 100;

            #region JSON

            const string auctionInfo = "{\"tradeId\":137221997781,\"buyNowPrice\":0,\"currentBid\":0,\"itemData\":{\"id\":105416364138,\"timestamp\":1380884038,\"itemType\":\"player\",\"rating\":83,\"untradeable\":false,\"injuryType\":\"hip\",\"injuryGames\":0,\"suspension\":0,\"morale\":50,\"fitness\":99,\"assists\":16,\"lastSalePrice\":4800,\"owners\":3,\"teamid\":34,\"resourceId\":1610770869,\"discardValue\":664,\"formation\":\"f343\",\"preferredPosition\":\"RM\",\"assetId\":158133,\"cardsubtypeid\":2,\"itemState\":\"forSale\",\"training\":0,\"attributeList\":[{\"value\":88,\"index\":0},{\"value\":77,\"index\":1},{\"value\":80,\"index\":2},{\"value\":85,\"index\":3},{\"value\":53,\"index\":4},{\"value\":65,\"index\":5}],\"statsList\":[{\"value\":42,\"index\":0},{\"value\":12,\"index\":1},{\"value\":0,\"index\":2},{\"value\":0,\"index\":3},{\"value\":0,\"index\":4}],\"lifetimeStats\":[{\"value\":48,\"index\":0},{\"value\":13,\"index\":1},{\"value\":0,\"index\":2},{\"value\":0,\"index\":3},{\"value\":0,\"index\":4}],\"contract\":0,\"rareflag\":1,\"playStyle\":250,\"lifetimeAssists\":20,\"loyaltyBonus\":1},\"watched\":null,\"bidState\":\"none\",\"tradeState\":\"active\",\"startingBid\":4800,\"offers\":0,\"expires\":15,\"sellerName\":\"dummy_value\",\"sellerEstablished\":1295121221,\"sellerId\":0}";
            var auctions = new List<string>();
            for (var i = 0; i < numberOfAuctions; i++)
            {
                auctions.Add(auctionInfo);
            }
            const string json = "{{\"auctionInfo\":[{0}],\"credits\":12345,\"bidTokens\":{{\"count\":25,\"updateTime\":0}},\"currencies\":[{{\"name\":\"COINS\",\"funds\":12345,\"finalFunds\":12345}},{{\"name\":\"TOKEN\",\"funds\":0,\"finalFunds\":0}},{{\"name\":\"POINTS\",\"funds\":0,\"finalFunds\":0}}],\"duplicateItemIdList\":null,\"errorState\":null}}";

            #endregion

            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, string.Format(json, string.Join(",", auctions)));
            _futClient.RequestFactories.SearchRequestFactory = parameters => new SearchRequest(parameters) { HttpClient = mock.Object, Resources = _resources };

            await _futClient.SearchAsync(new PlayerSearchParameters());
            var stopwatch = Stopwatch.StartNew();

            for (var i = 0; i < iterations; i++)
            {
                await _futClient.SearchAsync(new PlayerSearchParameters());
            }

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            var millisecondsPerIteration = 1.0 * elapsedMilliseconds / iterations;

            Console.WriteLine("Iterations: {0}\nAuctions: {1}\nElapsed: {2} ms\nAverage: {3} ms\nIterations per second: {4}",
                iterations, numberOfAuctions, elapsedMilliseconds, millisecondsPerIteration, 1000 / millisecondsPerIteration);
        }
    }
}
