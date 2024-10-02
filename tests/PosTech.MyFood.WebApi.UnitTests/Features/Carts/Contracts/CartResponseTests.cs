using PosTech.MyFood.WebApi.Features.Carts.Contracts;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Carts.Contracts;

public class CartResponseTests
{
    [Fact]
    public void ShouldInitializeCorrectly()
    {
        var id = Guid.NewGuid();
        var customerId = "12345678901";
        var items = new List<CartItemDto>
        {
            new() { ProductId = Guid.NewGuid(), ProductName = "Product 1", UnitPrice = 10.99m, Quantity = 1 }
        };

        var response = new CartResponse { CartId = id, CustomerId = customerId, Items = items };

        response.CartId.Should().Be(id);
        response.CustomerId.Should().Be(customerId);
        response.Items.Should().BeEquivalentTo(items);
        response.Items.Should().HaveCount(1);
        response.Items[0].ProductId.Should().Be(items[0].ProductId);
        response.Items[0].ProductName.Should().Be(items[0].ProductName);
        response.Items[0].UnitPrice.Should().Be(items[0].UnitPrice);
        response.Items[0].Quantity.Should().Be(items[0].Quantity);
    }

    [Fact]
    public void ShouldGetAndSetPropertiesCorrectly()
    {
        var response = new CartResponse();

        var id = Guid.NewGuid();
        var customerId = "12345678901";
        var items = new List<CartItemDto>
        {
            new() { ProductId = Guid.NewGuid(), ProductName = "Product 1", UnitPrice = 10.99m, Quantity = 1 }
        };

        response.CartId = id;
        response.CustomerId = customerId;
        response.Items = items;

        response.CartId.Should().Be(id);
        response.CustomerId.Should().Be(customerId);
        response.Items.Should().HaveCount(1);
        response.Items[0].ProductId.Should().Be(items[0].ProductId);
        response.Items[0].ProductName.Should().Be(items[0].ProductName);
        response.Items[0].UnitPrice.Should().Be(items[0].UnitPrice);
        response.Items[0].Quantity.Should().Be(items[0].Quantity);
    }
}