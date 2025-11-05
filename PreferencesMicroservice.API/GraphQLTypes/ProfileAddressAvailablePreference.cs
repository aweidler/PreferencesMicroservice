using System.Text.Json.Serialization;
using PreferencesMicroservice.API.Entity;

namespace PreferencesMicroservice.API.GraphQLTypes
{
    public class ProfileAddressAvailablePreference
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("houseNumber")]
        public string houseNumber { get; set; }

        [JsonPropertyName("postalCode")]
        public string postalCode { get; set; }

        [JsonPropertyName("street")]
        public string street { get; set; }

        [JsonPropertyName("city")]
        public string city { get; set; }

        [JsonPropertyName("country")]
        public string country { get; set; }

        [JsonPropertyName("notAtHome")]
        public NotAtHomePreference notAtHome { get; set; }

        [JsonPropertyName("deliveryMethod")]

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DeliveryMethod deliveryMethod { get; set; }
    }
}
