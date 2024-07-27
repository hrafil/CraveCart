using CraveCart.Domain.Entities;
using CraveCart.Domain.Specifications;

namespace CraveCart.Domain.Abstractions
{
    public interface IFoodTrucksRepository
    {
        Task<List<FoodTruck>> GetFoodTrucksAsync(FoodTruckFilter filter);

        Task InitializeAsync(List<FoodTruck> foodTrucks);
    }
}
