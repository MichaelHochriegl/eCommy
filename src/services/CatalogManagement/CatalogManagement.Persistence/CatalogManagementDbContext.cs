using CatalogManagement.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogManagement.Persistence;

public class CatalogManagementDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<CatalogItem> CatalogItems => Set<CatalogItem>();
    public DbSet<CatalogBrand> CatalogBrands => Set<CatalogBrand>();
    public DbSet<CatalogType> CatalogTypes => Set<CatalogType>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogManagementDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}