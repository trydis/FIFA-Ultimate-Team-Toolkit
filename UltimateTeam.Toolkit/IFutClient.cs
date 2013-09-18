using System.Threading.Tasks;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit
{
    public interface IFutClient
    {
        Task<LoginResponse> LoginAsync(LoginDetails loginDetails);
    }
}
