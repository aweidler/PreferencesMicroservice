using System.Text.Json.Serialization;

namespace PreferencesMicroservice.API.GraphQLTypes
{
    public class NotAtHomePreference
    {
        [JsonPropertyName("postNLPoint")]
        public string postNLPoint { get; set; }

        [JsonPropertyName("parcelLocker")]
        public string parcelLocker { get; set; }
    }
}
