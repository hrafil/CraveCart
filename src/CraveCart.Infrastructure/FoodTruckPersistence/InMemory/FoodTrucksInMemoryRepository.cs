using CraveCart.Domain.Abstractions;
using CraveCart.Domain.Entities;
using CraveCart.Domain.Specifications;
using System.Collections.Concurrent;

namespace CraveCart.Infrastructure.FoodTruckPersistence.InMemory
{
    internal class FoodTrucksInMemoryRepository : IFoodTrucksRepository
    {
        public ConcurrentBag<FoodTruck> FoodTrucks { get; set; } = [];

        public Task InitializeAsync(List<FoodTruck> foodTrucks)
        {
            // TODO: Provide lock to ensure collection wont be accessed while its initializing
            FoodTrucks.Clear();
            foreach (var foodTruck in foodTrucks)
            {
                FoodTrucks.Add(foodTruck);
            }
            return Task.CompletedTask;
        }

        public Task<List<FoodTruck>> GetFoodTrucksAsync(FoodTruckFilter filter)
        {
            return Task.FromResult(FoodTrucks.Where(filter.IsSatisfied).ToList());
        }
    }
}
