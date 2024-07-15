using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogManagement.Persistence.Entities;

public class CatalogBrand
{
    // TODO: This should later be reworked to a DB-friendly Type
    public Guid Id { get; set; }
    public required string BrandName { get; set; }
}

internal class CatalogBrandEntityTypeConfiguration : IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.HasKey(x => x.Id);
    }
}