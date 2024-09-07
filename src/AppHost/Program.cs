using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

#region CatalogManagement

var dbPassword = builder.AddParameter("CatalogManagementDbPassword", true);

var postgresServer = builder.AddPostgres(ServiceDescriptors.CatalogManagementDbServer, password: dbPassword);
var postgresDb = postgresServer.AddDatabase(ServiceDescriptors.CatalogManagementDb);
postgresServer.WithDataVolume(ServiceDescriptors.CatalogManagementDbVolume);

builder.AddProject<Projects.CatalogManagement_Api>(ServiceDescriptors.CatalogManagementApi)
    .WithReference(postgresDb);

#endregion


builder.Build().Run();