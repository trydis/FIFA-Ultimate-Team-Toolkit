using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ReListRequest : FutRequestBase, IFutRequest<RelistResponse>
    {
        public async Task<RelistResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.Auctionhouse + Resources.ReList;
            var content = new StringContent(" ");
            Task<HttpResponseMessage> reListMessageTask;

            AddCommonHeaders();
            reListMessageTask = HttpClient.PutAsync(uriString, content);

            var reListMessage = await reListMessageTask.ConfigureAwait(false);
            reListMessage.EnsureSuccessStatusCode();

            return await DeserializeAsync<RelistResponse>(reListMessage);
        }
    }
}
