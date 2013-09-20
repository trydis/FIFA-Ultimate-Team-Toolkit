using System;
using NUnit.Framework;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Factories;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Tests
{
    [TestFixture]
    public class FutClientTests
    {
        private FutRequestFactories _requestFactories;

        private IFutClient _futClient;

        public void CreateClient(bool passNullInstance = false)
        {
            _requestFactories = new FutRequestFactories();
            _futClient = new FutClient(passNullInstance ? null : _requestFactories);
        }

        [Test,
        ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenPassedNull_ShouldThrowArgumentNullException()
        {
            CreateClient(true);
        }

        [Test,
        ExpectedException(typeof(ArgumentNullException))]
        public async void LoginAsync_WhenPassedNull_ShouldThrowArgumentException()
        {
            CreateClient();

            await _futClient.LoginAsync(null);
        }

        [Test]
        public async void LoginAsync_WhenCalled_ShouldPerformRequest()
        {
            CreateClient();
            var mockRequest = TestHelpers.CreateMockRequestReturningNull<LoginResponse>();
            _requestFactories.LoginRequestFactory = details => mockRequest.Object;

            await _futClient.LoginAsync(TestHelpers.CreateValidLoginDetails());

            mockRequest.VerifyAll();
        }

        [Test,
        ExpectedException(typeof(FutException))]
        public async void LoginAsync_WhenPerformRequestThrowsException_ShouldThrowFutException()
        {
            CreateClient();
            var mockRequest = TestHelpers.CreateMockRequestThrowingException<LoginResponse>();
            _requestFactories.LoginRequestFactory = details => mockRequest.Object;

            await _futClient.LoginAsync(TestHelpers.CreateValidLoginDetails());

            mockRequest.VerifyAll();
        }
    }
}
