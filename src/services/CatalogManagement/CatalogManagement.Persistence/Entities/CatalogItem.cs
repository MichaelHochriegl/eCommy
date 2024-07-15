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
    
    // TODO: add nav prop to `CatalogBrand` and `CatalogType`
}