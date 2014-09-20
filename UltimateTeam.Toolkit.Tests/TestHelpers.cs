using System;
using System.Linq.Expressions;
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

        public static Mock<IFutRequest<T>> CreateMockFutRequestReturning<T>(T result) where T : class
        {
            var mock = new Mock<IFutRequest<T>>();
            mock.Setup(request => request.PerformRequestAsync())
                .Returns(() => Task.FromResult(result));

            return mock;
        }

        public static Mock<IFutRequest<T>> CreateMockFutRequestReturningNull<T>() where T : class
        {
            var mock = new Mock<IFutRequest<T>>();
            mock.Setup(request => request.PerformRequestAsync())
                .Returns(() => Task.FromResult(default(T)));

            return mock;
        }

        public static Mock<IFutRequest<T>> CreateMockFutRequestThrowingException<T>()
        {
            var mock = new Mock<IFutRequest<T>>();
            mock.Setup(request => request.PerformRequestAsync())
                .Throws(new HttpRequestException("Things went south..."));

            return mock;
        }
        
        public static Mock<IHttpClient> CreateMockHttpClientReturningJson(HttpMethod method, string json)
        {
            var mock = new Mock<IHttpClient>();
            Expression<Func<IHttpClient, Task<HttpResponseMessage>>> getExpression = client => client.GetAsync(It.IsAny<string>());
            Expression<Func<IHttpClient, Task<HttpResponseMessage>>> postExpression = client => client.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>());
            mock.Setup(method == HttpMethod.Get ? getExpression : postExpression)
                .Returns(() => Task.FromResult(new HttpResponseMessage { Content = new StringContent(json) }));

            return mock;
        }
    }
}
