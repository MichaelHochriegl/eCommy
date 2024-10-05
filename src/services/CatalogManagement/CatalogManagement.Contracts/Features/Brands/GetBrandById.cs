namespace CatalogManagement.Contracts.Features.Brands;

public record GetBrandByIdRequestV1(Guid Id);

public record GetBrandByIdResponseV1(Guid Id, string BrandName);