using System.Threading.Tasks;
using Newtonsoft.Json;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class CreditsRequest : FutRequestBase, IFutRequest<CreditsResponse>
    {
        public async Task<CreditsResponse> PerformRequestAsync()
        {
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(NonStandardHttpHeaders.MethodOverride, "GET");
            AddCommonHeaders();
            var creditsResponseMessage = await HttpClient.GetAsync(string.Format(Resources.FutHome + Resources.Credits));
            creditsResponseMessage.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<CreditsResponse>(await creditsResponseMessage.Content.ReadAsStringAsync());
        }
    }
}
