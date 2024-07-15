var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CatalogManagement_Api>("CatalogManagementApi");


builder.Build().Run();