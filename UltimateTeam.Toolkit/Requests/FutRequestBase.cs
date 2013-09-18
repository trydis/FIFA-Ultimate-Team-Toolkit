using System.Net;
using System.Net.Http;

namespace UltimateTeam.Toolkit.Requests
{
    internal abstract class FutRequestBase
    {
        protected readonly HttpClient HttpClient;

        private static readonly CookieContainer CookieContainer = new CookieContainer();

        protected FutRequestBase()
        {
            var messageHandler = new HttpClientHandler
            {
                CookieContainer = CookieContainer,
                AutomaticDecompression = DecompressionMethods.GZip
            };
            HttpClient = new HttpClient(messageHandler);
            HttpClient.DefaultRequestHeaders.ExpectContinue = false;
        }
    }
}