using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
   internal class SendItemToClubRequest : FutRequestBase, IFutRequest<SendItemToClubResponse>
    {
        private readonly ItemData _itemData;

        public SendItemToClubRequest(ItemData itemData)
        {
            itemData.ThrowIfNullArgument();
            _itemData = itemData;
        }

        public async Task<SendItemToClubResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Put);
            AddCommonHeaders();
            //{"itemData":[{"pile":"club","id":"01010101010101"}]}
            var content = string.Format("{{\"itemData\":[{{\"pile\":\"club\",\"id\":\"{0}\"}}]}}", _itemData.Id);
            var clubResponseMessage = await HttpClient
                .PostAsync(string.Format(Resources.FutHome + Resources.ListItem), new StringContent(content))
                .ConfigureAwait(false);

            return await Deserialize<SendItemToClubResponse>(clubResponseMessage);
        }
    }
  }

