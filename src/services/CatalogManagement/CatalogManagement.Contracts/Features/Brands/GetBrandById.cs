namespace CatalogManagement.Contracts.Features.Brands;

public record GetBrandByIdRequest(Guid Id);

public record GetBrandByIdResponse(Guid Id, string BrandName);