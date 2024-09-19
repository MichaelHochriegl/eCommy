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
    .SwaggerDocument(o =>
    {
        o.MaxEndpointVersion = 1;
        o.DocumentSettings = s =>
        {
            s.DocumentName = "v1";
            s.Title = "Catalog Management API";
            s.Version = "v1";
        };
    });

var app = builder.Build();
app.UseFastEndpoints(options =>
{
    options.Endpoints.RoutePrefix = "api";
    options.Endpoints.Configurator = definition => definition.AllowAnonymous();
    options.Versioning.Prefix = "v";
    options.Versioning.DefaultVersion = 1;
    options.Versioning.PrependToRoute = true;
});
app.UseSwaggerGen();

app.Run();