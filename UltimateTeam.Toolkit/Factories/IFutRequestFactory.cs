using System.Threading.Tasks;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Factories
{
    public interface IFutRequestFactory
    {
        Task CreateLoginRequest(LoginDetails loginDetails);
    }
}