using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ClubItemRequest : FutRequestBase, IFutRequest<ClubItemResponse>
    {
        private readonly uint _listType;
        // _listType:
        //  1 = Players
        // 100 = Staff
        // 142 = ClubItems
        // Consumables are different
        public ClubItemRequest()
        {
            _listType = 1;
        }

        public async Task<ClubItemResponse> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.MyClub + "&type={0}",_listType);
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var clubItemResponseMessage = await HttpClient
                .GetAsync(uriString)
                .ConfigureAwait(false);

            return await Deserialize<ClubItemResponse>(clubItemResponseMessage);
        }
    }
}
