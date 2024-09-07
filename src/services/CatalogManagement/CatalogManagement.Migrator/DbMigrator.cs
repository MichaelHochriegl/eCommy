namespace CatalogManagement.Migrator;

public class DbMigrator : BackgroundService
{
    private readonly ILogger<DbMigrator> _logger;

    public DbMigrator(ILogger<DbMigrator> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}