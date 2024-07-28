using PosTech.MyFood.Features.Products.Entities;
using PosTech.MyFood.WebApi.Features.Products.Contracts;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Products.Contracts;

public class ListProductsResponseTests
{
    [Fact]
    public void ListProductsResponse_ShouldInitializeCorrectly_WhenProductsListIsProvided()
    {
        var products = new List<ProductResponse>
        {
            new()
            {
                Id = Guid.NewGuid(), Name = "Product 1", Description = "Description 1", Price = 10.99m,
                Category = ProductCategory.Lanche, ImageUrl = "http://example.com/image1.jpg"
            },
            new()
            {
                Id = Guid.NewGuid(), Name = "Product 2", Description = "Description 2", Price = 20.99m,
                Category = ProductCategory.Bebida, ImageUrl = "http://example.com/image2.jpg"
            }
        };

        var response = new ListProductsResponse { Products = products };

        response.Should().NotBeNull();
        response.Products.Should().NotBeNull();
        response.Products.Should().HaveCount(2);
        response.Products[0].Name.Should().Be("Product 1");
        response.Products[1].Name.Should().Be("Product 2");
    }
}