using CatalogManagement.Persistence;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<CatalogManagementDbContext>(ServiceDescriptors.CatalogManagementDb);

builder.Services.AddFastEndpoints(options =>
{
    options.IncludeAbstractValidators = true;
})
.SwaggerDocument();

var app = builder.Build();
app.UseFastEndpoints(options =>
{
    options.Endpoints.Configurator = definition => definition.AllowAnonymous();
});
app.UseSwaggerGen();

app.Run();