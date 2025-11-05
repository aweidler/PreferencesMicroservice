using Amazon.DynamoDBv2.DataModel;

namespace PreferencesMicroservice.API.Entity
{
    [DynamoDBTable("ProfileAddressAvailablePreferences")]
    public class ProfileAddressAvailablePreferenceEntity
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        public string houseNumber { get; set; }

        public string postalCode { get; set; }

        public string street { get; set; }

        public string city { get; set; }

        public string country { get; set; }

        public NotAtHomePreferencesEntity notAtHome { get; set; }

        public DeliveryMethod deliveryMethod { get; set; }
    }
}
