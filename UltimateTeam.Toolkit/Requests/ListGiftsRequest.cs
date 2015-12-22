using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ListGiftsRequest : FutRequestBase, IFutRequest<ListGiftsResponse>
    {
        public async Task<ListGiftsResponse> PerformRequestAsync()
        {
            AddCommonHeaders();
            AddMethodOverrideHeader(HttpMethod.Get);
            var responseMessage = await HttpClient
                .GetAsync(string.Format(Resources.FutHome + Resources.Gifts))
                .ConfigureAwait(false);

            return await Deserialize<ListGiftsResponse>(responseMessage);
        }
    }
}
