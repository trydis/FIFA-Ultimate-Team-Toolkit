using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class StoreRequest : FutRequestBase, IFutRequest<StoreResponse>
    {
        public async Task<StoreResponse> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.Store);

            AddCommonHeaders();
            var storeResponseMessage = await HttpClient
                .GetAsync(uriString)
                .ConfigureAwait(false);

            return await DeserializeAsync<StoreResponse>(storeResponseMessage);
        }
    }
}