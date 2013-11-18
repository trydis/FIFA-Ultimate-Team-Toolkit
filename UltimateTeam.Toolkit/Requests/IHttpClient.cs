using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace UltimateTeam.Toolkit.Requests
{
    public interface IHttpClient
    {       
        HttpClientHandler MessageHandler { get; set; }

        void ClearRequestHeaders();

        void AddRequestHeader(string name, string value);

        void RemoveRequestHeader(string name);

        void AddConnectionKeepAliveHeader();

        void SetReferrerUri(string value);

        Task<HttpResponseMessage> GetAsync(string requestUri);

        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent httpContent);

        Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent httpContent);

        Task<byte[]> GetByteArrayAsync(string requestUri);
    }
}
