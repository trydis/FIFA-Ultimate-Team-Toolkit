using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ListGiftsRequest : FutRequestBase, IFutRequest<ListGiftsResponse>
    {
        public async Task<ListGiftsResponse> PerformRequestAsync()
        {
            if (AppVersion != AppVersion.WebApp)
            {
                throw new FutException($"Not implemented for {AppVersion}");
            }

            AddCommonHeaders(HttpMethod.Get);
            var responseMessage = await HttpClient
                .GetAsync(Resources.FutHome + Resources.ActiveMessageList)
                .ConfigureAwait(false);

            return await DeserializeAsync<ListGiftsResponse>(responseMessage);
        }
    }
}
