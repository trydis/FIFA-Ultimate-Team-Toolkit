using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;
using System;
using UltimateTeam.Toolkit.Exceptions;

namespace UltimateTeam.Toolkit.Requests
{
   internal class SendItemToClubRequest : FutRequestBase, IFutRequest<SendItemToClubResponse>
    {
        private readonly ItemData _itemData;
        private AppVersion _appVersion;

        public SendItemToClubRequest(ItemData itemData)
        {
            itemData.ThrowIfNullArgument();
            _itemData = itemData;
        }

        public async Task<SendItemToClubResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddMethodOverrideHeader(HttpMethod.Put);
                AddCommonHeaders();

                var content = string.Format("{{\"itemData\":[{{\"pile\":\"club\",\"id\":\"{0}\"}}]}}", _itemData.Id);
                var clubResponseMessage = await HttpClient
                    .PostAsync(string.Format(Resources.FutHome + Resources.ListItem), new StringContent(content))
                    .ConfigureAwait(false);

                return await Deserialize<SendItemToClubResponse>(clubResponseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();

                var content = string.Format("{{\"itemData\":[{{\"pile\":\"club\",\"id\":\"{0}\"}}]}}", _itemData.Id);
                var clubResponseMessage = await HttpClient
                    .PutAsync(string.Format(Resources.FutHome + Resources.ListItem + Resources.TradePile + "?_=" + DateTimeExtensions.ToUnixTime(DateTime.Now)), new StringContent(content))
                    .ConfigureAwait(false);

                return await Deserialize<SendItemToClubResponse>(clubResponseMessage);
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", appVersion.ToString()));
            }
        }
     }
  }

