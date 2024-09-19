using CatalogManagement.Contracts.Features.Brands;
using CatalogManagement.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace CatalogManagement.Api.Features.Brands.GetAllBrands;

public class GetAllBrandsEndpointV1 : EndpointWithoutRequest<GetAllBrandsResponseV1>
{
    private readonly CatalogManagementDbContext _dbContext;

    public GetAllBrandsEndpointV1(CatalogManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public override void Configure()
    {
        Get("/brands");
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var brands = await _dbContext.CatalogBrands
            .Select(brand => new BrandDtoV1(brand.Id, brand.BrandName))
            .ToListAsync(cancellationToken: ct);
        
        await SendOkAsync(new GetAllBrandsResponseV1(brands), ct);
    }
}