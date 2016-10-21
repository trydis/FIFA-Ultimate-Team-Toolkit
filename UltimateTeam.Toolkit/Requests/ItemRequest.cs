using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ItemRequest : FutRequestBase, IFutRequest<Item>
    {
        private readonly long _assetId;

        public ItemRequest(long assetId)
        {
            _assetId = assetId;
        }

        public async Task<Item> PerformRequestAsync()
        {
            if (AppVersion == AppVersion.WebApp)
            {
                AddAnonymousHeader();
            }
            else
            {
                AddAnonymousMobileHeader();
            }

            var itemResponseMessage = await HttpClient
                                                .GetAsync(string.Format(Resources.Item, _assetId))
                                                .ConfigureAwait(false);
            var itemWrapper = await DeserializeAsync<ItemWrapper>(itemResponseMessage);

            return itemWrapper.Item;
        }
    }
}
