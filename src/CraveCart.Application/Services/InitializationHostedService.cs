using CraveCart.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CraveCart.Application.Services
{
    // TODO: Provide functionality to peridocally chech for new food trucks and update the data
    // TODO: Provide functionality for caching the last data so the service wont need to initalize each time upon startup
    public class InitializationHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger _logger;

        public InitializationHostedService(IServiceScopeFactory serviceScopeFactory, ILogger<InitializationHostedService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Initializing food truck data...");
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                using var scope = _serviceScopeFactory.CreateScope();
                var foodTrucksProviderService = scope.ServiceProvider.GetRequiredService<IFoodTrucksProviderService>();
                var foodTrucksRepository = scope.ServiceProvider.GetRequiredService<IFoodTrucksRepository>();

                var foodTrucks = await foodTrucksProviderService.GetFoodTrucksAsync(cancellationToken);
                await foodTrucksRepository.InitializeAsync(foodTrucks);

                stopwatch.Stop();
                _logger.LogInformation("Food truck data initialized in {Miliseconds}ms.", stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                // TODO: Better solution is to provide mechanism for the service to recover from such error - due to time constraints not implemented
                _logger.LogCritical(ex, "Error occurred during initialization of food truck data! Service cannot be started! See log for information");
                throw;
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await InitializeAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
