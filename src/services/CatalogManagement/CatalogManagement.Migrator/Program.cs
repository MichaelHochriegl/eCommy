using CatalogManagement.Migrator;
using CatalogManagement.Persistence;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<DbMigrator>();

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<CatalogManagementDbContext>(ServiceDescriptors.CatalogManagementDb);

// TODO:
// 1. Add proper DB connection to allow for Migration creation
// 2. Setup Migration creation
// 3. Adjust worker to apply actual migrations

var host = builder.Build();
host.Run();