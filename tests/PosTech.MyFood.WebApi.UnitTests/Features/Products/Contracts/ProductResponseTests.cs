using PosTech.MyFood.Features.Products.Entities;
using PosTech.MyFood.WebApi.Features.Products.Contracts;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Products.Contracts;

public class ProductResponseTests
{
    [Fact]
    public void ProductResponse_ShouldInitializeCorrectly_WhenValidParameters()
    {
        var id = Guid.NewGuid();
        var name = "Test Product";
        var description = "Test Description";
        var price = 10.99m;
        var category = ProductCategory.Lanche;
        var imageUrl = "http://example.com/image.jpg";

        var productResponse = new ProductResponse
        {
            Id = id,
            Name = name,
            Description = description,
            Price = price,
            Category = category,
            ImageUrl = imageUrl
        };

        productResponse.Should().NotBeNull();
        productResponse.Id.Should().Be(id);
        productResponse.Name.Should().Be(name);
        productResponse.Description.Should().Be(description);
        productResponse.Price.Should().Be(price);
        productResponse.Category.Should().Be(category);
        productResponse.ImageUrl.Should().Be(imageUrl);
    }
}