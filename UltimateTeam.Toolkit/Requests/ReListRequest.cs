using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

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
            uriString += $"?_={DateTime.Now.ToUnixTime()}";
            reListMessageTask = HttpClient.PutAsync(uriString, content);

            var reListMessage = await reListMessageTask.ConfigureAwait(false);
            reListMessage.EnsureSuccessStatusCode();

            return await DeserializeAsync<RelistResponse>(reListMessage);
        }
    }
}
