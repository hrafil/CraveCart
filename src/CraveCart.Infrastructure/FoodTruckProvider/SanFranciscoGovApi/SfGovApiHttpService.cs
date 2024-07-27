using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace CraveCart.Infrastructure.FoodTruckProvider.SanFranciscoGovApi
{
    internal class SfGovApiHttpService
    {
        private readonly HttpClient _sharedClient;
        private readonly ILogger _logger;
        private readonly SfGovApiConfiguration _settings;

        public SfGovApiHttpService(ILogger<SfGovApiHttpService> logger, SfGovApiConfiguration settings)
        {
            _logger = logger;
            _settings = settings;
            _sharedClient = CreateHttpClient();
        }

        private HttpClient CreateHttpClient()
        {
            if (string.IsNullOrEmpty(_settings.BaseUrl))
                throw new ArgumentException($"BaseUrl is required for {nameof(SfGovApiHttpService)}!");

            return new HttpClient(new SfGovApiHttpLoggingHandler(new HttpClientHandler(), _logger))
            {
                BaseAddress = new Uri(_settings.BaseUrl!),
                Timeout = TimeSpan.FromSeconds(10)
            };
        }

        public async Task<List<FoodTruckPermit>> GetFoodTrucksPermitsAsync(CancellationToken cancellationToken)
        {
            using var response = await _sharedClient.GetAsync("resource/rqzj-sfat.json", cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<FoodTruckPermit>>(cancellationToken) ?? throw new ArgumentException($"Cannot convert received data to {nameof(List<FoodTruckPermit>)}!");
        }
    }

    internal class SfGovApiHttpLoggingHandler : DelegatingHandler
    {
        private readonly ILogger _logger;

        public SfGovApiHttpLoggingHandler(HttpMessageHandler innerHandler, ILogger logger)
            : base(innerHandler)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await base.SendAsync(request, cancellationToken);
                var logObject = new
                {
                    Request = request.ToString(),
                    RequestContent = request.Content != null ? await request.Content.ReadAsStringAsync(CancellationToken.None) : "NoContent",
                    Response = response.ToString(),
                    ResponseContent = response.Content != null ? await response.Content.ReadAsStringAsync(CancellationToken.None) : "NoContent"
                };

                if (response.IsSuccessStatusCode)
                    _logger.LogInformation("SfGovApi successfull request: {RequestInfo}", JsonSerializer.Serialize(logObject));
                if (!response.IsSuccessStatusCode)
                    _logger.LogError("SfGovApi request error: {RequestInfo}", JsonSerializer.Serialize(logObject));

                return response;
            }
            catch (Exception ex)
            {
                var logObject = new
                {
                    Request = request.ToString(),
                    RequestContent = request.Content != null ? await request.Content.ReadAsStringAsync(CancellationToken.None) : "NoContent",
                };
                _logger.LogError(ex, "SfGovApi request failed: {RequestInfo}", JsonSerializer.Serialize(logObject));
                throw;
            }
        }
    }
}
