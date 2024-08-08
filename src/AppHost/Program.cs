using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

#region CatalogManagement

var postgresServer = builder.AddPostgres(ServiceDescriptors.CatalogManagementDbServer);
var postgresDb = postgresServer.AddDatabase(ServiceDescriptors.CatalogManagementDb);

builder.AddProject<Projects.CatalogManagement_Api>(ServiceDescriptors.CatalogManagementApi)
    .WithReference(postgresDb);

#endregion


builder.Build().Run();