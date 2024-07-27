using CraveCart.Domain.Entities;

namespace CraveCart.Domain.Specifications;

public class FoodTruckFilter
{
    private readonly FoodTruckFacilityType? _facilityType;
    private readonly FoodTruckLicenseStatus? _licenseStatus;

    public FoodTruckFilter(FoodTruckLicenseStatus? licenseStatus, FoodTruckFacilityType? facilityType)
    {
        _facilityType = facilityType;
        _licenseStatus = licenseStatus;
    }

    public bool IsSatisfied(FoodTruck foodTruck)
    {
        if (foodTruck.FacilityType != _facilityType && _facilityType != null)
        {
            return false;
        }

        if (foodTruck.LicenseStatus != _licenseStatus && _licenseStatus != null)
        {
            return false;
        }

        return true;
    }
}
