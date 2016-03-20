using System.Threading.Tasks;

namespace UltimateTeam.Toolkit.Requests
{
    public interface IFutRequest<TResponse>
    {
        Task<TResponse> PerformRequestAsync();
    }
}