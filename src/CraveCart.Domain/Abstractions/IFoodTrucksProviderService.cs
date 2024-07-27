using CraveCart.Domain.Entities;

namespace CraveCart.Domain.Abstractions
{
    public interface IFoodTrucksProviderService
    {
        Task<List<FoodTruck>> GetFoodTrucksAsync(CancellationToken cancellationToken);
    }
}
