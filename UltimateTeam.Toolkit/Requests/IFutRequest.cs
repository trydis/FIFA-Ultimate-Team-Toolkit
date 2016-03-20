using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;

namespace UltimateTeam.Toolkit.Requests
{
    public interface IFutRequest<TResponse>
    {
        Task<TResponse> PerformRequestAsync(AppVersion appVersion);
    }
}