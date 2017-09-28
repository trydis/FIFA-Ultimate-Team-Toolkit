using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;

namespace UltimateTeam.Toolkit.Requests
{
    internal class RemoveSoldItemsFromTradePileRequest : FutRequestBase, IFutRequest<byte>
    {
        public async Task<byte> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.RemoveFromTradePile, "sold");
            Task<HttpResponseMessage> removeFromTradePileMessageTask;

            AddCommonHeaders();
            removeFromTradePileMessageTask = HttpClient.DeleteAsync(uriString);

            var removeFromTradePileMessage = await removeFromTradePileMessageTask.ConfigureAwait(false);
            removeFromTradePileMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}