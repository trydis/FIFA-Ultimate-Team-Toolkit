using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SendItemToTradePileRequest : FutRequestBase, IFutRequest<SendItemToTradePileResponse>
    {
        private readonly ItemData _itemData;

        public SendItemToTradePileRequest(ItemData itemData)
        {
            itemData.ThrowIfNullArgument();
            _itemData = itemData;
        }

        public async Task<SendItemToTradePileResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Put);
            AddCommonHeaders();
            var content = string.Format("{{\"itemData\":[{{\"id\":\"{0}\",\"pile\":\"trade\"}}]}}", _itemData.Id);
            var tradepileResponseMessage = await HttpClient
                .PostAsync(string.Format(Resources.FutHome + Resources.ListItem), new StringContent(content))
                .ConfigureAwait(false);

            return await Deserialize<SendItemToTradePileResponse>(tradepileResponseMessage);
        }
    }
}
