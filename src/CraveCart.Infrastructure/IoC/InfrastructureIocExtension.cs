using CraveCart.Domain.Abstractions;
using CraveCart.Infrastructure.FoodTruckPersistence.InMemory;
using CraveCart.Infrastructure.FoodTruckProvider.SanFranciscoGovApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CraveCart.Infrastructure.IoC
{
    public static class InfrastructureIocExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var sfGovApiConf = configuration.SafeGet<SfGovApiConfiguration>(nameof(SfGovApiConfiguration));
            services.AddSingleton(sfGovApiConf);
            services.AddSingleton<SfGovApiHttpService>();
            services.AddSingleton<IFoodTrucksRepository, FoodTrucksInMemoryRepository>();
            services.AddTransient<IFoodTrucksProviderService, SfGovApiFoodTrucksProviderService>();
        }
    }
}
