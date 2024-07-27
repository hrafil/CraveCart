using CraveCart.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CraveCart.Application.Ioc
{
    public static class ApplicationIocExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<InitializationHostedService>();
            services.AddHostedService<InitializationHostedService>();

            services.AddScoped<IFoodTruckSearchService, FoodTruckSearchService>();
        }
    }
}
