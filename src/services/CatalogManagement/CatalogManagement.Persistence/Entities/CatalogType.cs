namespace CatalogManagement.Persistence.Entities;

public class CatalogType
{
    // TODO: This should later be reworked to a DB-friendly Type
    public Guid Id { get; set; }
    public required string Type { get; set; }
}