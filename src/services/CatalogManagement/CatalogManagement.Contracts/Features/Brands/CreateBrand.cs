namespace CatalogManagement.Contracts.Features.Brands;

public record CreateBrandRequest(string BrandName);

public record CreateBrandResponse(Guid Id, string BrandName);