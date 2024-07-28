using Bogus;
using PosTech.MyFood.Features.Products.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.WebApi.UnitTests.Mocks;

public static class OrderItemMocks
{
    public static OrderItem GenerateValidOrderItem()
    {
        var faker = new Faker();
        var orderItemId = new OrderItemId(faker.Random.Guid());
        var orderId = new OrderId(faker.Random.Guid());
        var productId = new ProductId(faker.Random.Guid());
        var productName = faker.Commerce.ProductName();
        var productDescription = faker.Commerce.ProductDescription();
        var unitPrice = faker.Random.Decimal(1, 1000);
        var quantity = faker.Random.Int(1, 100);
        var category = faker.PickRandom<ProductCategory>();

        return OrderItem.Create(orderItemId, orderId, productId, productName, productDescription, unitPrice, quantity,
            category);
    }

    public static OrderItem GenerateInvalidOrderItem()
    {
        return OrderItem.Create(new OrderItemId(Guid.Empty), new OrderId(Guid.Empty), new ProductId(Guid.Empty),
            string.Empty, string.Empty, -1, 0, ProductCategory.Lanche);
    }
}