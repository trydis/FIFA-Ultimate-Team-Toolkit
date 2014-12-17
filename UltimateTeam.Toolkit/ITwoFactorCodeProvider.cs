using System.Threading.Tasks;

namespace UltimateTeam.Toolkit
{
    public interface ITwoFactorCodeProvider
    {
        Task<string> GetTwoFactorCodeAsync();
    }
}
