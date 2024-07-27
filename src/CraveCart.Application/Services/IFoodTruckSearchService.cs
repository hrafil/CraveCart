using CraveCart.Application.Dto;

namespace CraveCart.Application.Services
{
    public interface IFoodTruckSearchService
    {
        Task<List<FoodTruckSearchResultItem>> SearchFoodTrucksAsync(FoodTruckSearchParameters searchParameters);
    }
}
