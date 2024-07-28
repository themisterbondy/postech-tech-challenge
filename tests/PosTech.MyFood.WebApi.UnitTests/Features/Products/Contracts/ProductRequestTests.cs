using PosTech.MyFood.Features.Products.Entities;
using PosTech.MyFood.WebApi.Features.Products.Contracts;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Products.Contracts;

public class ProductRequestTests
{
    [Fact]
    public void ProductRequest_ShouldInitializeCorrectly_WhenValidParameters()
    {
        var name = "Test Product";
        var description = "Test Description";
        var price = 10.99m;
        var category = ProductCategory.Lanche;
        var imageUrl = "http://example.com/image.jpg";

        var productRequest = new ProductRequest
        {
            Name = name,
            Description = description,
            Price = price,
            Category = category,
            ImageUrl = imageUrl
        };

        productRequest.Should().NotBeNull();
        productRequest.Name.Should().Be(name);
        productRequest.Description.Should().Be(description);
        productRequest.Price.Should().Be(price);
        productRequest.Category.Should().Be(category);
        productRequest.ImageUrl.Should().Be(imageUrl);
    }
}