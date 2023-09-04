using System.Net.Http;

namespace UrlShortner.Handler
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ApplicationContext _applicationContext;
        public LoggingHandler(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        protected  override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            string requestContent = string.Empty;
            if (request.Content != null)             
            {
                requestContent = await request.Content.ReadAsStringAsync();
            }

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            string ResponseContent = string.Empty;
            if (response.Content != null)
            {
                ResponseContent = await response.Content.ReadAsStringAsync();
            }

            _applicationContext.ApiLogs.Add(new Entities.ApiLog
            {
                RequestContent = requestContent,
                ResponseContent = ResponseContent
            });

            await _applicationContext.SaveChangesAsync();

            return response;
        }
    }
}
