using PreferencesMicroservice.API.GraphQLTypes;

namespace PreferencesMicroservice.API.Services
{
    public interface IProfileAddressAvailableAddressPreferenceService
    {
        Task<IEnumerable<ProfileAddressAvailablePreference>> GetPreferences();
        Task<ProfileAddressAvailablePreference> GetPreference(string id);
        Task<ProfileAddressAvailablePreference> CreatePreference(ProfileAddressAvailablePreference newPreference);
        Task<ProfileAddressAvailablePreference> DeletePreference(string id);
    }
}
