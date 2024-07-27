using CraveCart.Application.Dto;
using CraveCart.Domain.Abstractions;
using CraveCart.Domain.Entities;
using Geolocation;
using Microsoft.Extensions.Logging;

namespace CraveCart.Application.Services
{
    internal class FoodTruckSearchService : IFoodTruckSearchService
    {
        private readonly IFoodTrucksRepository _foodTrucksRepository;
        private readonly ILogger _logger;

        public FoodTruckSearchService(IFoodTrucksRepository foodTrucksRepository, ILogger<FoodTruckSearchService> logger)
        {
            _foodTrucksRepository = foodTrucksRepository;
            _logger = logger;
        }

        public async Task<List<FoodTruckSearchResultItem>> SearchFoodTrucksAsync(FoodTruckSearchParameters searchParameters)
        {
            var foodTrucks = await _foodTrucksRepository.GetFoodTrucksAsync();

            // TODO: Consider more efficient full text search
            var trucksByFood = foodTrucks.Where(x => x.FoodItems?.Contains(searchParameters.PrefferedFood!, StringComparison.InvariantCultureIgnoreCase) == true).ToList();
            return GetTrucksByDistance(searchParameters.Latitude, searchParameters.Longitude, searchParameters.MaxCountOfTrucks, trucksByFood);
        }

        private List<FoodTruckSearchResultItem> GetTrucksByDistance(double latitude, double longtitude, int maxCountOfTrucks, List<FoodTruck> foodTrucks)
        {
            // TODO: Consider optimizing the algorithm
            Coordinate origin = new(latitude, longtitude);
            var orderedTrucks = new SortedDictionary<double, FoodTruckSearchResultItem>();

            foreach (var truck in foodTrucks)
            {
                try
                {
                    Coordinate destination = new(truck.Latitude, truck.Longitude);
                    double distance = GeoCalculator.GetDistance(origin, destination, 5, DistanceUnit.Kilometers);
                    orderedTrucks.Add(distance, MapToSearchResultItem(truck, distance));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while calculating distance for truck address: {Address} latitude: {Latitude} longitude {Longitude}", truck.Address, truck.Latitude, truck.Longitude);
                }
            }

            return orderedTrucks.Take(maxCountOfTrucks).Select(x => x.Value).ToList();
        }

        private static FoodTruckSearchResultItem MapToSearchResultItem(FoodTruck truck, double distance)
        {
            return new FoodTruckSearchResultItem
            {
                Address = truck.Address,
                Applicant = truck.Applicant,
                FoodItems = truck.FoodItems,
                DistanceKm = distance,
                Latitude = truck.Latitude,
                Longitude = truck.Longitude
            };
        }
    }
}
