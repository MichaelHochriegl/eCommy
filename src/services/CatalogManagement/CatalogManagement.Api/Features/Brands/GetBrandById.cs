using CatalogManagement.Contracts.Features.Brands;
using CatalogManagement.Persistence;
using FastEndpoints;

namespace CatalogManagement.Api.Features.Brands;

public class GetBrandByIdEndpoint : Endpoint<GetBrandByIdRequest, GetBrandByIdResponse>
{
    private readonly CatalogManagementDbContext _dbContext;

    public GetBrandByIdEndpoint(CatalogManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("brands/{id}");
    }

    public override async Task HandleAsync(GetBrandByIdRequest req, CancellationToken ct)
    {
        var brand = await _dbContext.CatalogBrands.FindAsync([req.Id], cancellationToken: ct);
        if (brand is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new GetBrandByIdResponse(brand.Id, brand.BrandName);
        await SendOkAsync(response, ct);
    }
}