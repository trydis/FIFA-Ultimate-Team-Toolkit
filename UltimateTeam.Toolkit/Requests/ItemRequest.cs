using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ItemRequest : FutRequestBase, IFutRequest<Item>
    {
        private readonly long _baseId;

        public ItemRequest(long baseId)
        {
            _baseId = baseId;
        }

        public async Task<Item> PerformRequestAsync()
        {
            AddAnonymousHeader("image/webp,image/apng,image/*,*/*;q=0.8");

            var itemResponseMessage = await HttpClient
                                                .GetAsync(string.Format(Resources.Item, _baseId))
                                                .ConfigureAwait(false);
            var itemWrapper = await DeserializeAsync<ItemWrapper>(itemResponseMessage);

            return itemWrapper.Item;
        }
    }
}
