using CatalogManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<CatalogManagementDbContext>(ServiceDescriptors.CatalogManagementDb);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();