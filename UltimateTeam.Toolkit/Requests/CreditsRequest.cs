using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class CreditsRequest : FutRequestBase, IFutRequest<CreditsResponse>
    {
        public async Task<CreditsResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.Credits;

            AddCommonHeaders();
            var creditsResponseMessage = await HttpClient
                .GetAsync(uriString)
                .ConfigureAwait(false);

            return await DeserializeAsync<CreditsResponse>(creditsResponseMessage);
        }
    }
}
