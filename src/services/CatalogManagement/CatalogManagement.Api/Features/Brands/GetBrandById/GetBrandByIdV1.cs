using CatalogManagement.Contracts.Features.Brands;
using CatalogManagement.Persistence;
using FastEndpoints;

namespace CatalogManagement.Api.Features.Brands.GetBrandById;

public class GetBrandByIdEndpointV1 : Endpoint<GetBrandByIdRequestV1, GetBrandByIdResponseV1>
{
    private readonly CatalogManagementDbContext _dbContext;

    public GetBrandByIdEndpointV1(CatalogManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("brands/{id}");
    }

    public override async Task HandleAsync(GetBrandByIdRequestV1 req, CancellationToken ct)
    {
        var brand = await _dbContext.CatalogBrands.FindAsync([req.Id], cancellationToken: ct);
        if (brand is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new GetBrandByIdResponseV1(brand.Id, brand.BrandName);
        await SendOkAsync(response, ct);
    }
}