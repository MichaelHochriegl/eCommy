using CatalogManagement.Migrator;
using CatalogManagement.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<DbMigrator>();

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<CatalogManagementDbContext>(ServiceDescriptors.CatalogManagementDb, configureDbContextOptions:
    optionsBuilder =>
    {
        optionsBuilder.UseNpgsql(options => options.MigrationsAssembly(typeof(DbMigrator).Assembly.FullName));
    });

var host = builder.Build();
host.Run();