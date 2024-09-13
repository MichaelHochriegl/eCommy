using Microsoft.Extensions.Hosting;
using Projects;

namespace eCommy.Tests.Integration.services.CatalogManagement;

public class CatalogManagementFixture : IAsyncLifetime
{
    public DistributedApplication? App { get; private set; }
    public HttpClient? Client { get; private set; }
    
    public async Task InitializeAsync()
    {
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(builder =>
        {
            builder.AddStandardResilienceHandler();
        });

        App = await appHost.BuildAsync();
        var resourceNotificationService = App.Services.GetRequiredService<ResourceNotificationService>();
        await App.StartAsync();
        Client = App.CreateHttpClient(ServiceDescriptors.CatalogManagementApi);
        await resourceNotificationService
            .WaitForResourceAsync(ServiceDescriptors.CatalogManagementMigrator, KnownResourceStates.Finished)
            .WaitAsync(TimeSpan.FromSeconds(30));
        await resourceNotificationService
            .WaitForResourceAsync(ServiceDescriptors.CatalogManagementApi, KnownResourceStates.Running)
            .WaitAsync(TimeSpan.FromSeconds(30));
    }


    public async Task DisposeAsync()
    {
        Client?.Dispose();

        if (App is not null)
        {
            await App.StopAsync();
            await App.DisposeAsync();
        }
    }
}

[CollectionDefinition("CatalogManagement")]
public class CatalogManagementCollection : ICollectionFixture<CatalogManagementFixture>
{
    
}