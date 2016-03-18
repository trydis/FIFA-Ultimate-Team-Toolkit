using System.Threading.Tasks;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    public interface IFutRequest<TResponse>
    {
        Task<TResponse> PerformRequestAsync(AppVersion appVersion);
    }
}