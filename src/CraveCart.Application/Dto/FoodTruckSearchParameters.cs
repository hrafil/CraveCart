using CraveCart.Domain.Entities;

namespace CraveCart.Application.Dto
{
    public class FoodTruckSearchParameters
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string? PrefferedFood { get; set; }

        public int MaxCountOfTrucks { get; set; }

        public FoodTruckFacilityType? FacilityType { get; set; }

        public FoodTruckLicenseStatus? LicenseStatus { get; set; }
    }
}
