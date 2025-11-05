using Amazon.Lambda.Annotations;
using Amazon.Lambda.AppSyncEvents;
using Amazon.Lambda.Core;
using AWS.Lambda.Powertools.Logging;
using PreferencesMicroservice.API.GraphQLTypes;
using PreferencesMicroservice.API.Services;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]


namespace PreferencesMicroservice.API
{
    public class Functions
    {
        private readonly IProfileAddressAvailableAddressPreferenceService _service;

        public Functions(IProfileAddressAvailableAddressPreferenceService service)
        {
            _service = service;
        }

        [LambdaFunction()]
        [Logging(LogEvent = true)]
        public async Task<IEnumerable<ProfileAddressAvailablePreference>> GetPreferences()
        {
            return await _service.GetPreferences();
        }

        [LambdaFunction()]
        [Logging(LogEvent = true)]
        public async Task<ProfileAddressAvailablePreference> GetPreference(AppSyncResolverEvent<Dictionary<string, string>> appSyncEvent)
        {
            var id = appSyncEvent.Arguments["id"].ToString();
            return await _service.GetPreference(id);
        }

        [LambdaFunction()]
        [Logging(LogEvent = true)]
        public async Task<ProfileAddressAvailablePreference> CreatePreference(AppSyncResolverEvent<ProfileAddressAvailablePreference> appSyncEvent)
        {
            return await _service.CreatePreference(appSyncEvent.Arguments);
        }

        [LambdaFunction]
        [Logging(LogEvent = true)]
        public async Task<ProfileAddressAvailablePreference> DeletePreference(AppSyncResolverEvent<Dictionary<string, string>> appSyncEvent)
        {
            var id = appSyncEvent.Arguments["id"].ToString();
            return await _service.DeletePreference(id);
        }
    }
}
