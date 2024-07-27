using CraveCart.Domain.Entities;

namespace CraveCart.Domain.Abstractions
{
    public interface IFoodTrucksRepository
    {
        Task<List<FoodTruck>> GetFoodTrucksAsync();

        Task InitializeAsync(List<FoodTruck> foodTrucks);
    }
}
