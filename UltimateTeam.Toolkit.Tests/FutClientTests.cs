using System;
using NUnit.Framework;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Tests
{
    [TestFixture]
    public class FutClientTests
    {
        private IFutClient _futClient;

        [SetUp]
        public void Setup()
        {
            _futClient = new FutClient();
        }

        [Test,
        ExpectedException(typeof(ArgumentNullException))]
        public async void LoginAsync_WhenPassedNull_ShouldThrowArgumentException()
        {
            await _futClient.LoginAsync(null);
        }

        [Test]
        public async void LoginAsync_WhenCalled_ShouldPerformRequest()
        {
            const string guid = "8194C855-BCCB-426F-A6F9-130383E9FB5A";
            var loginResponse = new LoginResponse(guid, new Shards(), new UserAccounts(), guid, guid);
            var mockRequest = TestHelpers.CreateMockRequestReturning(loginResponse);
            _futClient.RequestFactories.LoginRequestFactory = details => mockRequest.Object;

            await _futClient.LoginAsync(TestHelpers.CreateValidLoginDetails());

            mockRequest.VerifyAll();
        }

        [Test,
        ExpectedException(typeof(FutException))]
        public async void LoginAsync_WhenPerformRequestThrowsException_ShouldThrowFutException()
        {
            var mockRequest = TestHelpers.CreateMockRequestThrowingException<LoginResponse>();
            _futClient.RequestFactories.LoginRequestFactory = details => mockRequest.Object;

            await _futClient.LoginAsync(TestHelpers.CreateValidLoginDetails());

            mockRequest.VerifyAll();
        }
    }
}
