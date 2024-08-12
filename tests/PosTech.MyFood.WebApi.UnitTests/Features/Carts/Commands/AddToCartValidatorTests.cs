using PosTech.MyFood.WebApi.Features.Carts.Commands;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Carts.Commands;

public class AddToCartValidatorTests
{
    private readonly AddToCart.AddToCartValidator _validator = new();

    [Fact]
    public void ShouldHaveErrorWhenCustomerIdIsInvalid()
    {
        var command = new AddToCart.Command { CustomerId = "123" };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenCustomerIdIsValid()
    {
        var command = new AddToCart.Command { CustomerId = "36697999071" };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.CustomerId);
    }

    [Fact]
    public void ShouldHaveErrorWhenProductIdIsEmpty()
    {
        var command = new AddToCart.Command { ProductId = Guid.Empty };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProductId);
    }

    [Fact]
    public void ShouldHaveErrorWhenQuantityIsZero()
    {
        var command = new AddToCart.Command { Quantity = 0 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenQuantityIsGreaterThanZero()
    {
        var command = new AddToCart.Command { Quantity = 1 };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Quantity);
    }
}