namespace CatalogManagement.Contracts.Features.Brands;

// TODO: think about introducing paging here?

public record GetAllBrandsResponseV1(IEnumerable<BrandDtoV1> Brands);

public record BrandDtoV1(Guid Id, string Name);