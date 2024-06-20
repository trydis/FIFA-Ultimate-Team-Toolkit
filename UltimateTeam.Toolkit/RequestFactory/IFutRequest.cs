namespace UltimateTeam.Toolkit.RequestFactory
{
    public interface IFutRequest<TResponse>
    {
        Task<TResponse> PerformRequestAsync();
    }
}