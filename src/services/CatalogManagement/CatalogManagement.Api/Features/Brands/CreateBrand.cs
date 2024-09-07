using CatalogManagement.Contracts.Features.Brands;
using CatalogManagement.Persistence;
using CatalogManagement.Persistence.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace CatalogManagement.Api.Features.Brands;

public class CreateBrandEndpoint : Endpoint<CreateBrandRequest, CreateBrandResponse>
{
    private readonly CatalogManagementDbContext _dbContext;

    public CreateBrandEndpoint(CatalogManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/brands");
    }

    public override async Task HandleAsync(CreateBrandRequest req, CancellationToken ct)
    {
        var alreadyPresent = await _dbContext.CatalogBrands.AnyAsync(b => b.BrandName == req.BrandName, ct);

        if (alreadyPresent)
        {
            ThrowError($"A brand with name '{req.BrandName}' already exists.", 409);
            return;
        }
        
        var brand = new CatalogBrand { Id = Guid.NewGuid(), BrandName = req.BrandName };

        _dbContext.CatalogBrands.Add(brand);
        await _dbContext.SaveChangesAsync(ct);

        var response = new CreateBrandResponse(brand.Id, brand.BrandName);
        await SendCreatedAtAsync<GetBrandByIdEndpoint>(new { brand.Id}, response, cancellation: ct);
    }
}