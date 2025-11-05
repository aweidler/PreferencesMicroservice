#pragma warning disable CA2252

var builder = DistributedApplication.CreateBuilder(args);

builder.AddAWSLambdaFunction<Projects.PreferencesMicroservice_Api>("GetPreferences",
       lambdaHandler: "PreferencesMicroservice.API::PreferencesMicroservice.API.Functions_GetPreferences_Generated::GetPreferences");
builder.AddAWSLambdaFunction<Projects.PreferencesMicroservice_Api>("GetPreference",
       lambdaHandler: "PreferencesMicroservice.API::PreferencesMicroservice.API.Functions_GetPreference_Generated::GetPreference");
builder.AddAWSLambdaFunction<Projects.PreferencesMicroservice_Api>("CreatePreference",
        lambdaHandler: "PreferencesMicroservice.API::PreferencesMicroservice.API.Functions_CreatePreference_Generated::CreatePreference");
builder.AddAWSLambdaFunction<Projects.PreferencesMicroservice_Api>("DeletePreference",
       lambdaHandler: "PreferencesMicroservice.API::PreferencesMicroservice.API.Functions_DeletePreference_Generated::DeletePreference");

builder.Build().Run();