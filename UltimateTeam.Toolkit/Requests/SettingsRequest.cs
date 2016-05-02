using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SettingsRequest : FutRequestBase, IFutRequest<SettingsResponse>
    {
        public async Task<SettingsResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.Settings;
            Task<HttpResponseMessage> settingsResponseMessageTask;

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Get);
                settingsResponseMessageTask = HttpClient.PostAsync(uriString, new StringContent(" "));
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"?_={DateTime.Now.ToUnixTime()}";
                settingsResponseMessageTask = HttpClient.GetAsync(uriString);
            }

            var settingsResponseMessage = await settingsResponseMessageTask.ConfigureAwait(false);

            return await DeserializeAsync<SettingsResponse>(settingsResponseMessage);
        }
    }
}