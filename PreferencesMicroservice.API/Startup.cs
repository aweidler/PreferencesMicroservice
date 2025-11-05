using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.DependencyInjection;
using PreferencesMicroservice.API.Repository;
using PreferencesMicroservice.API.Services;

namespace PreferencesMicroservice.API
{
    [Amazon.Lambda.Annotations.LambdaStartup]
    internal class Startup
    {
        /*
        / Services for Lambda functions can be registered in the services dependency injection container in this method
        */
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>(p => new DynamoDBContext(new AmazonDynamoDBClient()));
            services.AddSingleton<IProfileAddressAvailableAddressPreferenceRepository, ProfileAddressAvailableAddressPreferenceRepository>();
            services.AddSingleton<IProfileAddressAvailableAddressPreferenceService, ProfileAddressAvailableAddressPreferenceService>();
        }
    }
}
