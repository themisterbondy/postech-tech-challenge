using PosTech.MyFood.WebApi.Features.Carts.Entities;
using PosTech.MyFood.WebApi.Features.Carts.Repositories;
using PosTech.MyFood.WebApi.UnitTests.Mocks;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Carts.Repositories;

public class CartRepositoryTests
{
    [Fact]
    public async Task GetByCustomerIdAsync_ReturnsNull_WhenCustomerIdDoesNotExist()
    {
        var context = ApplicationDbContextMock.Create();
        var repository = new CartRepository(context);

        var result = await repository.GetByCustomerIdAsync("nonexistent-customer-id");

        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByCustomerIdAsync_ReturnsCart_WhenCustomerIdExists()
    {
        var context = ApplicationDbContextMock.Create();
        var cart = CartMocks.GenerateValidCart();
        context.Carts.Add(cart);
        await context.SaveChangesAsync();
        var repository = new CartRepository(context);

        var result = await repository.GetByCustomerIdAsync(cart.CustomerId);

        result.Should().NotBeNull();
        result.CustomerId.Should().Be(cart.CustomerId);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsFalse_WhenCartDoesNotExist()
    {
        var context = ApplicationDbContextMock.Create();
        var repository = new CartRepository(context);

        var result = await repository.ExistsAsync(new CartId(Guid.NewGuid()));

        result.Should().BeFalse();
    }

    [Fact]
    public async Task ExistsAsync_ReturnsTrue_WhenCartExists()
    {
        var context = ApplicationDbContextMock.Create();
        var cart = CartMocks.GenerateValidCart();
        context.Carts.Add(cart);
        await context.SaveChangesAsync();
        var repository = new CartRepository(context);

        var result = await repository.ExistsAsync(cart.Id);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task AddAsync_AddsCartSuccessfully()
    {
        var context = ApplicationDbContextMock.Create();
        var cart = CartMocks.GenerateValidCart();
        var repository = new CartRepository(context);

        await repository.AddAsync(cart);

        context.Carts.Should().Contain(cart);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesCartSuccessfully()
    {
        var context = ApplicationDbContextMock.Create();
        var cart = CartMocks.GenerateValidCart();
        context.Carts.Add(cart);
        await context.SaveChangesAsync();
        var repository = new CartRepository(context);

        cart.CustomerId = "updated-customer-id";
        await repository.UpdateAsync(cart);

        context.Carts.First(c => c.Id == cart.Id).CustomerId.Should().Be("updated-customer-id");
    }

    [Fact]
    public async Task DeleteCartsOlderThanAsync_DeletesOldCarts()
    {
        var context = ApplicationDbContextMock.Create();
        var oldCart = CartMocks.GenerateOldCart();
        context.Carts.Add(oldCart);
        await context.SaveChangesAsync();
        var repository = new CartRepository(context);

        await repository.DeleteUnpaidCartsOlderThanAsync(DateTime.UtcNow);

        context.Carts.Should().NotContain(c => c.Id == oldCart.Id);
    }
}