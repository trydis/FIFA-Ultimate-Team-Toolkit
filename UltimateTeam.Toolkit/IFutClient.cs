using System.Threading.Tasks;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;
using UltimateTeam.Toolkit.Requests;

namespace UltimateTeam.Toolkit
{
    public interface IFutClient
    {
        Task<LoginResponse> LoginAsync(LoginDetails loginDetails);

        Task<AuctionResponse> SearchAsync(SearchParameters searchParameters);
    }
}
