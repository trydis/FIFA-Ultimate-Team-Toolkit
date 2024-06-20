using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PurchasePackRequest : FutRequestBase, IFutRequest<PurchasedPackResponse>
    {
        private int _packId;
        private CurrencyOption _currencyOption;

        public PurchasePackRequest(int packId, CurrencyOption currencyOption)
        {
            _packId = packId;
            _currencyOption = currencyOption;
        }

        public async Task<PurchasedPackResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.PurchasedItems;
            var content = $"{{\"currency\":\"{_currencyOption}\",\"packId\":{_packId}}}";

            AddCommonHeaders();
            var purchasedItemsMessage = await HttpClient
                .PostAsync(uriString, new StringContent(content))
                .ConfigureAwait(false);

            return await DeserializeAsync<PurchasedPackResponse>(purchasedItemsMessage);
        }
    }
}
