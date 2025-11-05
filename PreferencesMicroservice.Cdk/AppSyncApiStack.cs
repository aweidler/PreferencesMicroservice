using Amazon.CDK;
using Amazon.CDK.AWS.AppSync;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.Lambda;
using Constructs;
using Attribute = Amazon.CDK.AWS.DynamoDB.Attribute;

namespace PreferencesMicroservice.Cdk
{
    public class AppSyncApiStack : Stack
    {
        private FunctionProps CreateLambdaProps(string handler, string tableName)
        {
            return new FunctionProps
            {
                Runtime = Runtime.DOTNET_8,
                Code = Amazon.CDK.AWS.Lambda.Code.FromAsset("PreferencesMicroservice.Api/bin/Release/net8.0/publish"),
                Handler = handler,
                MemorySize = 256,
                Timeout = Duration.Seconds(30),
                Environment = new Dictionary<string, string>
                {
                    { "DYNAMODB_TABLE", tableName },
                    { "POWERTOOLS_SERVICE_NAME", "PreferencesMicroservice" },
                    { "POWERTOOLS_METRICS_NAMESPACE", "PreferencesMicroservice" }
                }
            };
        }

        public AppSyncApiStack(Construct scope, string id, IStackProps? props = null) : base(scope, id, props)
        {
            var preferencesTable = new Table(this, "PreferencesTable", new TableProps
            {
                TableName = "ProfileAddressAvailablePreferences",
                PartitionKey = new Attribute { Name = "Id", Type = AttributeType.STRING },
                BillingMode = BillingMode.PAY_PER_REQUEST,
                RemovalPolicy = RemovalPolicy.DESTROY
            });

            var api = new GraphqlApi(this, "PreferencesApi", new GraphqlApiProps
            {
                Name = "postnl-preferences-api",
                Definition = Definition.FromFile("PreferencesMicroservice.Cdk/graphql/schema.graphql"),
                AuthorizationConfig = new AuthorizationConfig
                {
                    DefaultAuthorization = new AuthorizationMode
                    {
                        AuthorizationType = AuthorizationType.API_KEY,
                        ApiKeyConfig = new ApiKeyConfig
                        {
                            Expires = Expiration.After(Duration.Days(30))
                        }
                    },
                },
                XrayEnabled = true
            });

            var getPreferencesFunction = new Function(this, "GetPreferencesFunction",
                CreateLambdaProps("PreferencesMicroservice.API::PreferencesMicroservice.API.Functions_GetPreferences_Generated::GetPreferences", preferencesTable.TableName));
            preferencesTable.GrantReadData(getPreferencesFunction);

            var getPreferenceFunction = new Function(this, "GetPreferenceFunction",
                CreateLambdaProps("PreferencesMicroservice.API::PreferencesMicroservice.API.Functions_GetPreference_Generated::GetPreference", preferencesTable.TableName));
            preferencesTable.GrantReadData(getPreferenceFunction);

            var createPreferenceFunction = new Function(this, "CreatePreferenceFunction",
                CreateLambdaProps("PreferencesMicroservice.API::PreferencesMicroservice.API.Functions_CreatePreference_Generated::CreatePreference", preferencesTable.TableName));
            preferencesTable.GrantReadWriteData(createPreferenceFunction);

            var deletePreferenceFunction = new Function(this, "DeletePreferenceFunction",
                CreateLambdaProps("PreferencesMicroservice.API::PreferencesMicroservice.API.Functions_DeletePreference_Generated::DeletePreference", preferencesTable.TableName));
            preferencesTable.GrantReadWriteData(deletePreferenceFunction);

            var getPreferenceDataSource = api.AddLambdaDataSource("GetPreferencesDataSource", getPreferencesFunction);
            var getPreferenceByIdDataSource = api.AddLambdaDataSource("GetPreferenceByIdDataSource", getPreferenceFunction);
            var createPreferenceDataSource = api.AddLambdaDataSource("CreatePreferenceDataSource", createPreferenceFunction);
            var deletePreferenceDataSource = api.AddLambdaDataSource("DeletePreferenceDataSource", deletePreferenceFunction);

            // Direct Lambda Resolvers
            getPreferenceDataSource.CreateResolver("listPreferencesResolver", new BaseResolverProps
            {
                TypeName = "Query",
                FieldName = "listPreferences"
            });

            getPreferenceByIdDataSource.CreateResolver("GetPreferenceByIdResolver", new BaseResolverProps
            {
                TypeName = "Query",
                FieldName = "getPreferenceById"
            });

            createPreferenceDataSource.CreateResolver("CreatePreferenceResolver", new BaseResolverProps
            {
                TypeName = "Mutation",
                FieldName = "createPreference"
            });

            deletePreferenceDataSource.CreateResolver("DeletePreferenceResolver", new BaseResolverProps
            {
                TypeName = "Mutation",
                FieldName = "deletePreference"
            });

            // Output our AppSync API URL and API Key, maybe this can be more secret in the future, lol
            new CfnOutput(this, "GraphQLAPIURL", new CfnOutputProps
            {
                Value = api.GraphqlUrl
            });

            new CfnOutput(this, "GraphQLAPIKey", new CfnOutputProps
            {
                Value = api.ApiKey ?? "No API Key"
            });
        }
    }
}