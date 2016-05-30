using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

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
            if (_itemData.CardSubTypeId == 231)
            {
                throw new FutException("CardSubTypeId 231 (Draft-Token or Credits) cannot be send to tradepile");
            }

            var uriString = Resources.FutHome + Resources.ListItem;
            var content = new StringContent($"{{\"itemData\":[{{\"id\":\"{_itemData.Id}\",\"pile\":\"trade\"}}]}}");
            Task<HttpResponseMessage> tradepileResponseMessageTask;

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(_itemData.CardSubTypeId == 231 ? HttpMethod.Post : HttpMethod.Put);
                tradepileResponseMessageTask = HttpClient.PostAsync(uriString, content);
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"?_={DateTime.Now.ToUnixTime()}";
                tradepileResponseMessageTask = HttpClient.PutAsync(uriString, content);
            }

            var tradepileResponseMessage = await tradepileResponseMessageTask.ConfigureAwait(false);

            return await DeserializeAsync<SendItemToTradePileResponse>(tradepileResponseMessage);
        }
    }
}
