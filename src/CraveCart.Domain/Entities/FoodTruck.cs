using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CraveCart.Domain.Entities
{
    public partial class FoodTruck
    {
        public string? Address { get; set; }

        public string? Applicant { get; set; }

        public string? FoodItems { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public FoodTruckFacilityType FacilityType { get; private set; }

        public FoodTruckLicenseStatus LicenseStatus { get; private set; }

        [GeneratedRegex(@"\s+")]
        private static partial Regex CleanWhitespaceRegex();

        public void SetFacilityType(string inputFacilityType)
        {
            var cleanedFacilityType = CleanWhitespaceRegex().Replace(inputFacilityType, "").ToLowerInvariant();
            FacilityType = cleanedFacilityType switch
            {
                "truck" => FoodTruckFacilityType.Truck,
                "pushcart" => FoodTruckFacilityType.Pushcart,
                _ => FoodTruckFacilityType.None
            };

            if (FacilityType == FoodTruckFacilityType.None)
            {
                throw new ArgumentException($"Cannot parse license status value: {inputFacilityType}");
            }
        }

        public void SetLicenseStatus(string inputLicenseStatus)
        {
            LicenseStatus = inputLicenseStatus.ToLowerInvariant().Trim() switch
            {
                "approved" => FoodTruckLicenseStatus.Approved,
                "requested" => FoodTruckLicenseStatus.Requested,
                "expired" => FoodTruckLicenseStatus.Expired,
                "suspend" => FoodTruckLicenseStatus.Suspend,
                _ => FoodTruckLicenseStatus.None
            };

            if (LicenseStatus == FoodTruckLicenseStatus.None)
            {
                throw new ArgumentException($"Cannot parse license status value: {inputLicenseStatus}");
            }
        }
    }
}
