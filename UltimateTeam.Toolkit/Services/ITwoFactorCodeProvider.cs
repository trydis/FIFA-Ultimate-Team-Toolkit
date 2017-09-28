using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;

namespace UltimateTeam.Toolkit.Services
{
    public interface ITwoFactorCodeProvider
    {
        Task<string> GetTwoFactorCodeAsync(AuthenticationType authType);
    }
}
