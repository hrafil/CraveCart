namespace CraveCart.Application.Dto
{
    public class FoodTruckSearchResultItem
    {
        public string? Applicant { get; set; }

        public string? Address { get; set; }

        public string? FoodItems { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double DistanceKm { get; set; }
    }
}
