using PreferencesMicroservice.API.Entity;

namespace PreferencesMicroservice.API.Repository
{
    public interface IProfileAddressAvailableAddressPreferenceRepository
    {
        Task<IEnumerable<ProfileAddressAvailablePreferenceEntity>> GetPreferences();
        Task<ProfileAddressAvailablePreferenceEntity> GetPreference(string id);
        Task CreatePreference(ProfileAddressAvailablePreferenceEntity preference);
        Task DeletePreference(ProfileAddressAvailablePreferenceEntity preference);
    }
}
