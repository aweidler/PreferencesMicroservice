using PreferencesMicroservice.API.Entity;
using PreferencesMicroservice.API.GraphQLTypes;
using PreferencesMicroservice.API.Repository;

namespace PreferencesMicroservice.API.Services
{
    public class ProfileAddressAvailableAddressPreferenceService: IProfileAddressAvailableAddressPreferenceService
    {
        private readonly IProfileAddressAvailableAddressPreferenceRepository _repository;

        public ProfileAddressAvailableAddressPreferenceService(IProfileAddressAvailableAddressPreferenceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProfileAddressAvailablePreference>> GetPreferences()
        {
            var entities = await _repository.GetPreferences();
            return entities.Select(ProfileAddressAvailableAddressPreferenceMapper.Preference);
        }

        public async Task<ProfileAddressAvailablePreference> GetPreference(string id)
        {
            var entity = await _repository.GetPreference(id);
            return ProfileAddressAvailableAddressPreferenceMapper.Preference(entity);
        }

        public async Task<ProfileAddressAvailablePreference> CreatePreference(ProfileAddressAvailablePreference newPreference)
        {
            var entity = new ProfileAddressAvailablePreferenceEntity
            {
                Id = Guid.NewGuid().ToString(), // This is where we would assign the user to connect to this Preference
                houseNumber = newPreference.houseNumber,
                postalCode = newPreference.postalCode,
                street = newPreference.street,
                city = newPreference.city,
                country = newPreference.country,
                notAtHome = new NotAtHomePreferencesEntity
                {
                    parcelLocker = newPreference.notAtHome.parcelLocker,
                    postNLPoint = newPreference.notAtHome.postNLPoint
                },
                deliveryMethod = newPreference.deliveryMethod
            };
            await _repository.CreatePreference(entity);
            return ProfileAddressAvailableAddressPreferenceMapper.Preference(entity);
        }

        public async Task<ProfileAddressAvailablePreference> DeletePreference(string id)
        {
            var entity = await _repository.GetPreference(id) ?? throw new Exception("Preference not found");
            await _repository.DeletePreference(entity);
            return ProfileAddressAvailableAddressPreferenceMapper.Preference(entity);
        }
    }
}
