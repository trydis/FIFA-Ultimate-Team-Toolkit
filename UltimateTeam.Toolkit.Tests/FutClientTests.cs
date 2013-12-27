using System;
using System.Linq;
using System.Net.Http;
using AssertExLib;
using NUnit.Framework;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Requests;

namespace UltimateTeam.Toolkit.Tests
{
    [TestFixture]
    public class FutClientTests
    {
        private IFutClient _futClient;
        private readonly Resources _resources = new Resources();

        [SetUp]
        public void Setup()
        {
            _futClient = new FutClient();
        }

        [Test]
        public void LoginAsync_WhenPassedNull_ShouldThrowArgumentNullException()
        {
            AssertEx.TaskThrows<ArgumentNullException>(async () => await _futClient.LoginAsync(null));
        }

        [Test]
        public async void LoginAsync_WhenCalled_ShouldPerformRequest()
        {
            const string dummyValue = "dummyValue";
            var loginResponse = new LoginResponse(dummyValue, new Shards(), new UserAccounts(), dummyValue, dummyValue);
            var mockRequest = TestHelpers.CreateMockFutRequestReturning(loginResponse);
            _futClient.RequestFactories.LoginRequestFactory = details => mockRequest.Object;

            await _futClient.LoginAsync(TestHelpers.CreateValidLoginDetails());

            mockRequest.VerifyAll();
        }

        [Test]
        public void LoginAsync_WhenPerformRequestThrowsException_ShouldThrowHttpRequestException()
        {
            var mockRequest = TestHelpers.CreateMockFutRequestThrowingException<LoginResponse>();
            _futClient.RequestFactories.LoginRequestFactory = details => mockRequest.Object;

            AssertEx.TaskThrows<HttpRequestException>(async () => await _futClient.LoginAsync(TestHelpers.CreateValidLoginDetails()));

            mockRequest.VerifyAll();
        }

        [Test]
        public void SearchAsync_WhenResponseContainsValidData_ShouldNotThrow()
        {
            #region JSON

            const string json = "{\"auctionInfo\":[{\"tradeId\":137221997781,\"buyNowPrice\":0,\"currentBid\":0,\"itemData\":{\"id\":105416364138,\"timestamp\":1380884038,\"itemType\":\"player\",\"rating\":83,\"untradeable\":false,\"injuryType\":\"hip\",\"injuryGames\":0,\"suspension\":0,\"morale\":50,\"fitness\":99,\"assists\":16,\"lastSalePrice\":4800,\"leagueId\":1,\"owners\":3,\"teamid\":34,\"resourceId\":1610770869,\"discardValue\":664,\"formation\":\"f343\",\"preferredPosition\":\"RM\",\"assetId\":158133,\"cardsubtypeid\":2,\"itemState\":\"forSale\",\"training\":0,\"attributeList\":[{\"value\":88,\"index\":0},{\"value\":77,\"index\":1},{\"value\":80,\"index\":2},{\"value\":85,\"index\":3},{\"value\":53,\"index\":4},{\"value\":65,\"index\":5}],\"statsList\":[{\"value\":42,\"index\":0},{\"value\":12,\"index\":1},{\"value\":0,\"index\":2},{\"value\":0,\"index\":3},{\"value\":0,\"index\":4}],\"lifetimeStats\":[{\"value\":48,\"index\":0},{\"value\":13,\"index\":1},{\"value\":0,\"index\":2},{\"value\":0,\"index\":3},{\"value\":0,\"index\":4}],\"contract\":0,\"rareflag\":1,\"playStyle\":250,\"lifetimeAssists\":20,\"loyaltyBonus\":1},\"watched\":null,\"bidState\":\"none\",\"tradeState\":\"active\",\"startingBid\":4800,\"offers\":0,\"expires\":15,\"sellerName\":\"dummy_value\",\"sellerEstablished\":1295121221,\"sellerId\":0}],\"credits\":12345,\"bidTokens\":{\"count\":25,\"updateTime\":0},\"currencies\":[{\"name\":\"COINS\",\"funds\":12345,\"finalFunds\":12345},{\"name\":\"TOKEN\",\"funds\":0,\"finalFunds\":0},{\"name\":\"POINTS\",\"funds\":0,\"finalFunds\":0}],\"duplicateItemIdList\":null,\"errorState\":null}";

            #endregion
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, json);
            _futClient.RequestFactories.SearchRequestFactory = parameters => new SearchRequest(parameters) { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskDoesNotThrow(async () => await _futClient.SearchAsync(new PlayerSearchParameters()));
        }

        [Test]
        public void SearchAsync_WhenResponseContainsPermissionDenied_ShouldThrowPermissionDeniedException()
        {
            const string jsonError = "{\"debug\":\"\",\"string\":\"Permission Denied\",\"reason\":\"\",\"code\":\"461\"}";
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, jsonError);
            _futClient.RequestFactories.SearchRequestFactory = parameters => new SearchRequest(parameters) { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskThrows<PermissionDeniedException>(async () => await _futClient.SearchAsync(new PlayerSearchParameters()));
        }

        [Test]
        public void SearchAsync_WhenResponseContainsExpiredSession_ShouldThrowExpiredSessionException()
        {
            const string jsonError = "{\"message\":null,\"reason\":\"expired session\",\"code\":401}";
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, jsonError);
            _futClient.RequestFactories.SearchRequestFactory = parameters => new SearchRequest(parameters) { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskThrows<ExpiredSessionException>(async () => await _futClient.SearchAsync(new PlayerSearchParameters()));
        }

        [Test]
        public void SearchAsync_WhenResponseContainsInternalServerError_ShouldThrowInternalServerException()
        {
            const string jsonError = "{\"debug\":\"\",\"string\":\"Internal Server Error (ut)\",\"reason\":\"\",\"code\":\"500\"}";
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, jsonError);
            _futClient.RequestFactories.SearchRequestFactory = parameters => new SearchRequest(parameters) { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskThrows<InternalServerException>(async () => await _futClient.SearchAsync(new PlayerSearchParameters()));
        }

        [Test]
        public void SearchAsync_WhenResponseContainsUnmappedCode_ShouldThrowFutException()
        {
            const string jsonError = "{\"message\":null,\"reason\":\"foo\",\"code\":999}";
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, jsonError);
            _futClient.RequestFactories.SearchRequestFactory = parameters => new SearchRequest(parameters) { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskThrows<FutException>(async () => await _futClient.SearchAsync(new PlayerSearchParameters()));
        }

        [Test]
        public void GetWatchlistAsync_WhenResponseContainsValidData_ShouldNotThrow()
        {
            #region JSON

            const string json = "{\"total\":1,\"auctionInfo\":[{\"tradeId\":137231545142,\"buyNowPrice\":0,\"tradeState\":\"closed\",\"offers\":0,\"itemData\":{\"id\":105863739699,\"timestamp\":1382222679,\"itemType\":\"player\",\"teamid\":11,\"rating\":78,\"lastSalePrice\":1000,\"leagueId\":1,\"owners\":2,\"morale\":50,\"formation\":\"f4321\",\"training\":0,\"untradeable\":false,\"preferredPosition\":\"ST\",\"assetId\":186146,\"itemState\":\"free\",\"resourceId\":1610798882,\"cardsubtypeid\":3,\"discardValue\":624,\"injuryType\":\"none\",\"injuryGames\":0,\"suspension\":0,\"fitness\":99,\"assists\":0,\"attributeList\":[{\"value\":85,\"index\":0},{\"value\":74,\"index\":1},{\"value\":73,\"index\":2},{\"value\":78,\"index\":3},{\"value\":45,\"index\":4},{\"value\":79,\"index\":5}],\"statsList\":[{\"value\":0,\"index\":0},{\"value\":0,\"index\":1},{\"value\":0,\"index\":2},{\"value\":0,\"index\":3},{\"value\":0,\"index\":4}],\"lifetimeStats\":[{\"value\":0,\"index\":0},{\"value\":0,\"index\":1},{\"value\":0,\"index\":2},{\"value\":0,\"index\":3},{\"value\":0,\"index\":4}],\"contract\":7,\"rareflag\":1,\"playStyle\":250,\"lifetimeAssists\":0,\"loyaltyBonus\":0},\"watched\":true,\"bidState\":\"outbid\",\"startingBid\":150,\"currentBid\":1000,\"expires\":-1,\"sellerName\":\"dummy_value\",\"sellerEstablished\":1310861136,\"sellerId\":0}],\"credits\":12345,\"duplicateItemIdList\":[{\"itemId\":105863739699,\"duplicateItemId\":105383426807}]}";

            #endregion
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Get, json);
            _futClient.RequestFactories.WatchlistRequestFactory = () => new WatchlistRequest { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskDoesNotThrow(async () => await _futClient.GetWatchlistAsync());
        }

        [Test]
        public void GetCreditsAsync_WhenResponseContainsValidData_ShouldNotThrow()
        {
            // TODO: Create one test without futCashBalance and one with it
            #region JSON

            const string json = "{\"currencies\":[{\"name\":\"COINS\",\"finalFunds\":12345,\"funds\":12345},{\"name\":\"TOKEN\",\"finalFunds\":0,\"funds\":0},{\"name\":\"POINTS\",\"finalFunds\":0,\"funds\":0}],\"credits\":12345,\"unopenedPacks\":{\"preOrderPacks\":0,\"recoveredPacks\":0},\"futCashBalance\":0.0}";

            #endregion
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Get, json);
            _futClient.RequestFactories.CreditsRequestFactory = () => new CreditsRequest { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskDoesNotThrow(async () => await _futClient.GetCreditsAsync());
        }

        [Test]
        public void GetTradePileAsync_WhenResponseContainsValidData_ShouldNotThrow()
        {
            // TODO: Add JSON containing auctions
            #region JSON

            const string json = "{\"bidTokens\":{},\"auctionInfo\":[],\"credits\":12345,\"currencies\":null,\"duplicateItemIdList\":null,\"errorState\":null}";

            #endregion
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Get, json);
            _futClient.RequestFactories.TradePileRequestFactory = () => new TradePileRequest { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskDoesNotThrow(async () => await _futClient.GetTradePileAsync());
        }

        [Test]
        public void GetTradeStatusAsync_WhenResponseContainsValidData_ShouldNotThrow()
        {
            #region JSON
            const string json = "{\"auctionInfo\":[{\"itemData\":{\"id\":105963173360,\"timestamp\":1382552392,\"itemType\":\"player\",\"untradeable\":false,\"discardValue\":624,\"resourceId\":1610632945,\"rating\":78,\"teamid\":461,\"lastSalePrice\":650,\"leagueId\":1,\"owners\":2,\"training\":0,\"cardsubtypeid\":1,\"injuryType\":\"none\",\"injuryGames\":0,\"suspension\":0,\"morale\":50,\"fitness\":99,\"assists\":0,\"itemState\":\"free\",\"formation\":\"f5212\",\"preferredPosition\":\"CB\",\"assetId\":20209,\"attributeList\":[{\"value\":64,\"index\":0},{\"value\":36,\"index\":1},{\"value\":47,\"index\":2},{\"value\":50,\"index\":3},{\"value\":77,\"index\":4},{\"value\":87,\"index\":5}],\"statsList\":[{\"value\":0,\"index\":0},{\"value\":0,\"index\":1},{\"value\":0,\"index\":2},{\"value\":0,\"index\":3},{\"value\":0,\"index\":4}],\"lifetimeStats\":[{\"value\":0,\"index\":0},{\"value\":0,\"index\":1},{\"value\":0,\"index\":2},{\"value\":0,\"index\":3},{\"value\":0,\"index\":4}],\"contract\":7,\"rareflag\":1,\"playStyle\":250,\"lifetimeAssists\":0,\"loyaltyBonus\":0},\"tradeId\":132303131726,\"buyNowPrice\":900,\"currentBid\":650,\"tradeState\":\"closed\",\"bidState\":\"none\",\"offers\":0,\"watched\":false,\"startingBid\":650,\"expires\":-1,\"sellerName\":\"dummy_value\",\"sellerEstablished\":1267136652,\"sellerId\":0}],\"credits\":12345,\"bidTokens\":{},\"currencies\":[{\"name\":\"COINS\",\"finalFunds\":12345,\"funds\":12345},{\"name\":\"TOKEN\",\"finalFunds\":0,\"funds\":0},{\"name\":\"POINTS\",\"finalFunds\":0,\"funds\":0}],\"duplicateItemIdList\":null,\"errorState\":null}";
            #endregion
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, json);
            _futClient.RequestFactories.TradeStatusRequestFactory = tradeIds => new TradeStatusRequest(tradeIds) { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskDoesNotThrow(async () => await _futClient.GetTradeStatusAsync(Enumerable.Empty<long>()));
        }

        [Test]
        public void GetItemAsync_WhenResponseContainsValidData_ShouldNotThrow()
        {
            #region JSON
            const string json = "{\"Item\":{\"FirstName\":\"Mario\",\"LastName\":\"Balotelli\",\"CommonName\":null,\"Height\":\"189\",\"DateOfBirth\":{\"Year\":\"1990\",\"Month\":\"8\",\"Day\":\"12\"},\"PreferredFoot\":\"Right\",\"ClubId\":\"47\",\"LeagueId\":\"31\",\"NationId\":\"27\",\"Rating\":\"84\",\"Attribute1\":\"84\",\"Attribute2\":\"82\",\"Attribute3\":\"67\",\"Attribute4\":\"85\",\"Attribute5\":\"48\",\"Attribute6\":\"75\",\"Rare\":\"1\",\"ItemType\":\"PlayerA\"}}";
            #endregion
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Get, json);
            _futClient.RequestFactories.ItemRequestFactory = auctionInfo => new ItemRequest(auctionInfo) { HttpClient = mock.Object };

            AssertEx.TaskDoesNotThrow(async () => await _futClient.GetItemAsync(new AuctionInfo { ItemData = new ItemData() }));
        }

        [Test]
        public void PlaceBidAsync_WhenResponseContainsValidData_ShouldNotThrow()
        {
            #region JSON
            const string json = "{\"auctionInfo\":[{\"itemData\":{\"id\":105532648599,\"timestamp\":1381164846,\"itemType\":\"player\",\"untradeable\":false,\"discardValue\":624,\"resourceId\":1610798882,\"rating\":78,\"teamid\":11,\"lastSalePrice\":950,\"leagueId\":1,\"owners\":4,\"training\":0,\"cardsubtypeid\":3,\"injuryType\":\"foot\",\"injuryGames\":0,\"suspension\":0,\"morale\":50,\"fitness\":99,\"assists\":2,\"itemState\":\"forSale\",\"formation\":\"f4222\",\"preferredPosition\":\"ST\",\"assetId\":186146,\"attributeList\":[{\"value\":85,\"index\":0},{\"value\":74,\"index\":1},{\"value\":73,\"index\":2},{\"value\":78,\"index\":3},{\"value\":45,\"index\":4},{\"value\":79,\"index\":5}],\"statsList\":[{\"value\":1,\"index\":0},{\"value\":0,\"index\":1},{\"value\":0,\"index\":2},{\"value\":0,\"index\":3},{\"value\":0,\"index\":4}],\"lifetimeStats\":[{\"value\":150,\"index\":0},{\"value\":91,\"index\":1},{\"value\":6,\"index\":2},{\"value\":0,\"index\":3},{\"value\":0,\"index\":4}],\"contract\":30,\"rareflag\":1,\"playStyle\":250,\"lifetimeAssists\":45,\"loyaltyBonus\":0},\"tradeId\":137303100579,\"buyNowPrice\":0,\"currentBid\":200,\"tradeState\":\"active\",\"bidState\":\"highest\",\"offers\":0,\"watched\":true,\"startingBid\":150,\"expires\":761,\"sellerName\":\"dummy_value\",\"sellerEstablished\":1381449221,\"sellerId\":0}],\"credits\":12345,\"bidTokens\":{\"count\":25,\"updateTime\":0},\"currencies\":[{\"name\":\"COINS\",\"finalFunds\":12345,\"funds\":12345},{\"name\":\"TOKEN\",\"finalFunds\":0,\"funds\":0},{\"name\":\"POINTS\",\"finalFunds\":0,\"funds\":0}],\"duplicateItemIdList\":[{\"itemId\":105532648599,\"duplicateItemId\":1053812426807}],\"errorState\":null}";
            #endregion
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, json);
            _futClient.RequestFactories.PlaceBidRequestFactory = (auctionInfo, bidAmount) =>
                new PlaceBidRequest(auctionInfo, bidAmount) { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskDoesNotThrow(async () => await _futClient.PlaceBidAsync(new AuctionInfo()));
        }

        [Test]
        public void PlaceBidAsync_WhenResponseContainsNotEnoughCreditError_ShouldThrowNotEnoughCreditException()
        {
            const string jsonError = "{\"debug\":\"\",\"string\":\"Not enough credit\",\"reason\":\"\",\"code\":\"470\"}";
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, jsonError); 
            _futClient.RequestFactories.PlaceBidRequestFactory = (auctionInfo, bidAmount) =>
                new PlaceBidRequest(auctionInfo, bidAmount) { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskThrows<NotEnoughCreditException>(async () => await _futClient.PlaceBidAsync(new AuctionInfo()));
        }

        [Test]
        public void PlaceBidAsync_WhenResponseContainsNoSuchTradeExistsError_ShouldThrowNoSuchTradeExistsException()
        {
            const string jsonError = "{\"debug\":\"\",\"string\":\"No such trade exists\",\"reason\":\"\",\"code\":\"478\"}";
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, jsonError);
            _futClient.RequestFactories.PlaceBidRequestFactory = (auctionInfo, bidAmount) =>
                new PlaceBidRequest(auctionInfo, bidAmount) { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskThrows<NoSuchTradeExistsException>(async () => await _futClient.PlaceBidAsync(new AuctionInfo()));
        }

        [Test]
        public void GetPurchasedItemsAsync_WhenResponseContainsValidData_ShouldNotThrow()
        {
            #region JSON
            const string json = "{\"itemData\":[{\"id\":104891626302,\"timestamp\":1379143263,\"itemType\":\"player\",\"teamid\":11,\"rating\":87,\"lastSalePrice\":15750,\"leagueId\":1,\"owners\":5,\"morale\":50,\"formation\":\"f352\",\"resourceId\":1610753337,\"untradeable\":false,\"injuryGames\":0,\"statsList\":[{\"value\":0,\"index\":0},{\"value\":0,\"index\":1},{\"value\":0,\"index\":2},{\"value\":0,\"index\":3},{\"value\":0,\"index\":4}],\"attributeList\":[{\"value\":53,\"index\":0},{\"value\":41,\"index\":1},{\"value\":58,\"index\":2},{\"value\":51,\"index\":3},{\"value\":86,\"index\":4},{\"value\":90,\"index\":5}],\"lifetimeStats\":[{\"value\":46,\"index\":0},{\"value\":3,\"index\":1},{\"value\":2,\"index\":2},{\"value\":0,\"index\":3},{\"value\":0,\"index\":4}],\"preferredPosition\":\"CB\",\"assetId\":140601,\"itemState\":\"free\",\"training\":0,\"discardValue\":696,\"cardsubtypeid\":1,\"injuryType\":\"none\",\"suspension\":0,\"fitness\":89,\"assists\":0,\"contract\":2,\"rareflag\":1,\"playStyle\":262,\"lifetimeAssists\":1,\"loyaltyBonus\":0}],\"duplicateItemIdList\":[{\"itemId\":106085451240,\"duplicateItemId\":105135793199}]}";
            #endregion
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Get, json);
            _futClient.RequestFactories.PurchasedItemsRequestFactory = () => new PurchasedItemsRequest { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskDoesNotThrow(async () => await _futClient.GetPurchasedItemsAsync());
        }

        [Test]
        public void ListAuctionAsync_WhenResponseContainsValidData_ShouldNotThrow()
        {
            #region JSON
            const string json = "{\"id\":136629153818}";
            #endregion
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, json);
            _futClient.RequestFactories.ListAuctionFactory = auctionDetails => new ListAuctionRequest(auctionDetails) { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskDoesNotThrow(async () => await _futClient.ListAuctionAsync(new AuctionDetails(1)));
        }

        [Test]
        public void ListAuctionAsync_WhenResponseContainsConflictServerError_ShouldThrowConflictException()
        {
            const string jsonError = "{\"debug\":\"\",\"string\":\"Conflict\",\"reason\":\"\",\"code\":\"409\"}";
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, jsonError);
            _futClient.RequestFactories.ListAuctionFactory = auctionDetails => new ListAuctionRequest(auctionDetails) { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskThrows<ConflictException>(async () => await _futClient.ListAuctionAsync(new AuctionDetails(1)));
        }

        [Test]
        public void ListAuctionAsync_WhenResponseContainsNotFoundServerError_ShouldThrowNotFoundException()
        {
            // The spelling error is intentional, although it doesn't matter for this test
            const string jsonError = "{\"debug\":\"\",\"string\":\"Not Fund\",\"reason\":\"\",\"code\":\"404\"}";
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, jsonError);
            _futClient.RequestFactories.ListAuctionFactory = auctionDetails => new ListAuctionRequest(auctionDetails) { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskThrows<NotFoundException>(async () => await _futClient.ListAuctionAsync(new AuctionDetails(1)));
        }

        [Test]
        public void QuickSellItemAsync_WhenResponseContainsValidData_ShouldNotThrow()
        {
            #region JSON
            const string json = "{\"items\":[{\"id\":104902666784}],\"totalCredits\":12345}";
            #endregion
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, json);
            _futClient.RequestFactories.QuickSellRequestFactory = auctionDetails => new QuickSellRequest(0) { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskDoesNotThrow(async () => await _futClient.QuickSellItemAsync(1));
        }

        [Test]
        public void SendItemToTradePileAsync_WhenResponseContainsValidData_ShouldNotThrow()
        {
            #region JSON
            const string json = "{\"itemData\":[{\"id\":105461757005,\"pile\":\"trade\",\"success\":true}]}";
            #endregion
            var mock = TestHelpers.CreateMockHttpClientReturningJson(HttpMethod.Post, json);
            _futClient.RequestFactories.SendItemToTradePileRequestFactory = itemData => new SendItemToTradePileRequest(itemData) { HttpClient = mock.Object, Resources = _resources };

            AssertEx.TaskDoesNotThrow(async () => await _futClient.SendItemToTradePileAsync(new ItemData()));
        }
    }
}