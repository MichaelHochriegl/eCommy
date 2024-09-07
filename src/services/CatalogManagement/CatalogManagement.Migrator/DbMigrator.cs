using System.Diagnostics;
using CatalogManagement.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using OpenTelemetry.Trace;

namespace CatalogManagement.Migrator;

public class DbMigrator : BackgroundService
{
    private readonly ILogger<DbMigrator> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostApplicationLifetime _lifetime;
    
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    public DbMigrator(ILogger<DbMigrator> logger, IServiceProvider serviceProvider, IHostApplicationLifetime lifetime)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _lifetime = lifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CatalogManagementDbContext>();
            
            await EnsureDatabase(dbContext, stoppingToken);
            await RunMigrations(dbContext, stoppingToken);
        }
        catch (Exception e)
        {
            activity?.RecordException(e);
            throw;
        }
        
        _lifetime.StopApplication();
    }

    private static async Task EnsureDatabase(CatalogManagementDbContext dbContext, CancellationToken cancellationToken)
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();
        
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);
            }
        });
    }
    
    private static async Task RunMigrations(CatalogManagementDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.Database.MigrateAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}