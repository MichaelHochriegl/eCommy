using CatalogManagement.Api.Features.Brands;
using CatalogManagement.Contracts.Features.Brands;

namespace eCommy.Tests.Integration.services.CatalogManagement.CatalogManagement.Api.Features.Brands;

[Collection("CatalogManagement")]
public class CreateBrandTests
{
    private readonly CatalogManagementFixture _fixture;
    private readonly HttpClient _client;

    public CreateBrandTests(CatalogManagementFixture fixture)
    {
        _fixture = fixture;
        _client = fixture.Client ?? throw new InvalidOperationException();
    }

    [Fact]
    public async Task Given_ValidBrand_Should_Create_Brand()
    {
        // Arrange
        var brand = new CreateBrandRequest("Example Brand");
        
        // Act
        var (rsp, res) = await _client.POSTAsync<CreateBrandEndpoint, CreateBrandRequest, CreateBrandResponse>(brand);

        // Assert
        rsp.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}