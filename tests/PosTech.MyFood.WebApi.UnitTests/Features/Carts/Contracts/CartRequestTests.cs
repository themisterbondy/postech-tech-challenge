using PosTech.MyFood.WebApi.Features.Carts.Contracts;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Carts.Contracts;

public class CartRequestTests
{
    [Fact]
    public void ShouldInitializeCorrectly()
    {
        var customerId = "12345678901";
        var productId = Guid.NewGuid();
        var quantity = 1;

        var request = new CartRequest { CustomerId = customerId, ProductId = productId, Quantity = quantity };

        request.CustomerId.Should().Be(customerId);
        request.ProductId.Should().Be(productId);
        request.Quantity.Should().Be(quantity);
    }

    [Fact]
    public void ShouldGetAndSetPropertiesCorrectly()
    {
        var request = new CartRequest();

        var customerId = "12345678901";
        var productId = Guid.NewGuid();
        var quantity = 1;

        request.CustomerId = customerId;
        request.ProductId = productId;
        request.Quantity = quantity;

        request.CustomerId.Should().Be(customerId);
        request.ProductId.Should().Be(productId);
        request.Quantity.Should().Be(quantity);
    }

    [Fact]
    public void ShouldAllowNullCustomerId()
    {
        var request = new CartRequest { CustomerId = null, ProductId = Guid.NewGuid(), Quantity = 1 };

        request.CustomerId.Should().BeNull();
    }

    [Fact]
    public void ShouldHaveDefaultValues()
    {
        var request = new CartRequest();

        request.CustomerId.Should().BeNull();
        request.ProductId.Should().Be(Guid.Empty);
        request.Quantity.Should().Be(0);
    }
}