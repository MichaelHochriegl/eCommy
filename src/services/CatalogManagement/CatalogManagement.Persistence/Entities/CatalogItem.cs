using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogManagement.Persistence.Entities;

public class CatalogItem
{
    private decimal _price;

    public Guid Id { get; set; }
    public required string Name { get; set; }

    public required decimal Price
    {
        get => _price;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            _price = value;
        }
    }

    public required uint AvailableStock { get; set; }

    public Guid CatalogBrandId { get; set; }
    public CatalogBrand Brand { get; set; }

    public Guid CatalogTypeId { get; set; }
    public CatalogType Type { get; set; }
}

internal class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasOne<CatalogBrand>(p => p.Brand);
        builder.HasOne<CatalogType>(p => p.Type);
    }
}