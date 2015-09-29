using System.Threading.Tasks;

namespace UltimateTeam.Toolkit.Services
{
    public interface ITwoFactorCodeProvider
    {
        Task<string> GetTwoFactorCodeAsync();
    }
}
