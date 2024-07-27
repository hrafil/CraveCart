using System.Text.Json.Serialization;

namespace CraveCart.Infrastructure.FoodTruckProvider.SanFranciscoGovApi
{
    public class FoodTruckPermit
    {
        [JsonPropertyName("locationid")]
        public string? LocationId { get; set; }

        [JsonPropertyName("Applicant")]
        public string? Applicant { get; set; }

        [JsonPropertyName("FacilityType")]
        public string? FacilityType { get; set; }

        [JsonPropertyName("LocationDescription")]
        public string? LocationDescription { get; set; }

        [JsonPropertyName("Address")]
        public string? Address { get; set; }

        [JsonPropertyName("blocklot")]
        public string? Blocklot { get; set; }

        [JsonPropertyName("block")]
        public string? Block { get; set; }

        [JsonPropertyName("lot")]
        public string? Lot { get; set; }

        [JsonPropertyName("permit")]
        public string? Permit { get; set; }

        [JsonPropertyName("Status")]
        public string? Status { get; set; }

        [JsonPropertyName("FoodItems")]
        public string? FoodItems { get; set; }

        [JsonPropertyName("X")]
        public string? X { get; set; }

        [JsonPropertyName("Y")]
        public string? Y { get; set; }

        [JsonPropertyName("Latitude")]
        public string? Latitude { get; set; }

        [JsonPropertyName("Longitude")]
        public string? Longitude { get; set; }

        [JsonPropertyName("Schedule")]
        public string? Schedule { get; set; }

        [JsonPropertyName("dayshours")]
        public string? DaysHours { get; set; }

        [JsonPropertyName("NOISent")]
        public DateTime NOISent { get; set; }
    }
}
