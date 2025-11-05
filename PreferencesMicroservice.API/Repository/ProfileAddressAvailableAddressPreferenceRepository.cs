using Amazon.DynamoDBv2.DataModel;
using PreferencesMicroservice.API.Entity;

namespace PreferencesMicroservice.API.Repository
{
    public class ProfileAddressAvailableAddressPreferenceRepository : IProfileAddressAvailableAddressPreferenceRepository
    {
        private readonly IDynamoDBContext _dynamoDBContext;

        public ProfileAddressAvailableAddressPreferenceRepository(IDynamoDBContext dynamoDBContext)
        {
            _dynamoDBContext = dynamoDBContext;
        }

        public async Task<IEnumerable<ProfileAddressAvailablePreferenceEntity>> GetPreferences()
        {
            var search = _dynamoDBContext.ScanAsync<ProfileAddressAvailablePreferenceEntity>(new List<ScanCondition>());
            return await search.GetRemainingAsync();
        }

        public async Task<ProfileAddressAvailablePreferenceEntity> GetPreference(string id)
        {
            var preference = await _dynamoDBContext.LoadAsync<ProfileAddressAvailablePreferenceEntity>(id);
            return preference;
        }

        public async Task CreatePreference(ProfileAddressAvailablePreferenceEntity preference)
        {
            await _dynamoDBContext.SaveAsync(preference);
        }

        public async Task DeletePreference(ProfileAddressAvailablePreferenceEntity preference)
        {
            await _dynamoDBContext.DeleteAsync(preference);
        }
    }
}
