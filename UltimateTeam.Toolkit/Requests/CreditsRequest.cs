using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class CreditsRequest : FutRequestBase, IFutRequest<CreditsResponse>
    {
        public async Task<CreditsResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var creditsResponseMessage = await HttpClient
                .GetAsync(string.Format(Resources.FutHome + Resources.Credits))
                .ConfigureAwait(false);

            return await Deserialize<CreditsResponse>(creditsResponseMessage);
        }
    }
}
