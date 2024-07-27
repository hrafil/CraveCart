using CraveCart.Domain.Abstractions;
using CraveCart.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CraveCart.Infrastructure.FoodTruckProvider.SanFranciscoGovApi
{
    internal class SfGovApiFoodTrucksProviderService : IFoodTrucksProviderService
    {
        private readonly SfGovApiHttpService _sfGovApiHttpService;
        private readonly ILogger _logger;

        public SfGovApiFoodTrucksProviderService(SfGovApiHttpService sfGovApiHttpService, ILogger<SfGovApiFoodTrucksProviderService> logger)
        {
            _sfGovApiHttpService = sfGovApiHttpService;
            _logger = logger;
        }

        public async Task<List<FoodTruck>> GetFoodTrucksAsync(CancellationToken cancellationToken)
        {
            var foodTrucksPermits = await _sfGovApiHttpService.GetFoodTrucksPermitsAsync(cancellationToken);
            if (foodTrucksPermits != null)
            {
                return ValidateAndConvertToFoodTruck(foodTrucksPermits);
            }
            throw new ArgumentException("Cannot convert received data to List<FoodTruck>!");
        }

        private List<FoodTruck> ValidateAndConvertToFoodTruck(List<FoodTruckPermit> foodTruckPermits)
        {
            var results = new List<FoodTruck>();
            foreach (var permit in foodTruckPermits)
            {
                try
                {
                    var foodTruck = new FoodTruck();

                    // Process Longtitude
                    if (!double.TryParse(permit.Longitude, out var longtitude))
                    {
                        throw new ArgumentException($"Cannot parse longtitude value: {permit.Longitude}");
                    }
                    foodTruck.Longitude = longtitude;

                    // Process Latitude
                    if (!double.TryParse(permit.Latitude, out var latitude))
                    {
                        throw new ArgumentException($"Cannot parse latitude value: {permit.Latitude}");
                    }
                    foodTruck.Latitude = latitude;

                    // Process FoodItems
                    if (string.IsNullOrEmpty(permit.FoodItems))
                    {
                        throw new ArgumentException($"{nameof(permit.FoodItems)} is null or empty");
                    }
                    foodTruck.FoodItems = permit.FoodItems;

                    // Process Address
                    if (string.IsNullOrEmpty(permit.Address))
                    {
                        throw new ArgumentException($"{permit.Address} is null or empty");
                    }
                    foodTruck.Address = permit.Address;

                    // Process Applicant
                    if (string.IsNullOrEmpty(permit.Applicant))
                    {
                        throw new ArgumentException($"{permit.Applicant} is null or empty");
                    }
                    foodTruck.Applicant = permit.Applicant;

                    results.Add(foodTruck);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Cannot convert permit to FoodTruck: {Permit}", permit.Address);
                }
            }
            return results;
        }
    }
}
