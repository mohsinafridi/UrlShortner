using UrlShortner.Models;

namespace UrlShortner.Services
{
    public interface IBoredApiClient
    {
        Task<ActivityModel> GetActivityAsyn(CancellationToken cancellationToken);
    }

    public class BoredApiClient : IBoredApiClient
    {
        private readonly HttpClient _client;
        public BoredApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<ActivityModel> GetActivityAsyn(CancellationToken cancellationToken)
        {
            var response = await this._client.GetAsync("activity", cancellationToken);
            return await response?.Content?.ReadFromJsonAsync<ActivityModel>();
        }
    }
}
