using UrlShortner.Models;

namespace UrlShortner.Services
{
    public interface IMyService
    {
        Task<ActivityModel> GetActivityAsyn(CancellationToken cancellationToken);
    }

    public class MyService : IMyService
    {

        private readonly IBoredApiClient _boredClient;
        public MyService(IBoredApiClient boredClient)
        {
            _boredClient = boredClient;
        }
        public async Task<ActivityModel> GetActivityAsyn(CancellationToken cancellationToken)
        {
           return await this._boredClient.GetActivityAsyn(cancellationToken);
        }
    }
    }
