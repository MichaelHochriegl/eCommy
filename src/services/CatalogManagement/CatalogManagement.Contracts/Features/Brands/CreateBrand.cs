namespace CatalogManagement.Contracts.Features.Brands;

public record CreateBrandRequestV1(string BrandName);

public record CreateBrandResponseV1(Guid Id, string BrandName);