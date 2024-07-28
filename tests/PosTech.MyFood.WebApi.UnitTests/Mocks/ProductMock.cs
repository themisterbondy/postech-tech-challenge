using Bogus;
using PosTech.MyFood.Features.Products.Entities;
using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.WebApi.UnitTests.Mocks;

public static class ProductMocks
{
    public static Product GenerateValidProduct()
    {
        var faker = new Faker();
        var productId = new ProductId(faker.Random.Guid());
        var name = faker.Commerce.ProductName();
        var description = faker.Commerce.ProductDescription();
        var price = faker.Random.Decimal(1, 1000);
        var category = ProductCategory.Lanche; // Assumindo que você tem um enum chamado ProductCategory
        var imageUrl = faker.Internet.Url();

        return Product.Create(productId, name, description, price, category, imageUrl);
    }

    public static Product GenerateInvalidProduct()
    {
        return Product.Create(null, null, null, 0, default, null);
    }
}