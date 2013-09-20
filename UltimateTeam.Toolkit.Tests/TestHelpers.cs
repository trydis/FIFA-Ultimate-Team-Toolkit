using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Requests;

namespace UltimateTeam.Toolkit.Tests
{
    public static class TestHelpers
    {
        private const string IgnoredParameter = "foo";

        public static LoginDetails CreateValidLoginDetails()
        {
            return new LoginDetails(IgnoredParameter, IgnoredParameter, IgnoredParameter, Platform.Ps3);
        }

        public static Mock<IFutRequest<T>> CreateMockRequestReturningNull<T>() where T : class
        {
            var mock = new Mock<IFutRequest<T>>();
            mock.Setup(request => request.PerformRequestAsync())
                .Returns(() => TaskEx.FromResult(default(T)));

            return mock;
        }

        public static Mock<IFutRequest<T>> CreateMockRequestThrowingException<T>()
        {
            var mock = new Mock<IFutRequest<T>>();
            mock.Setup(request => request.PerformRequestAsync())
                .Throws(new HttpRequestException());

            return mock;
        }
    }
}
