using PosTech.MyFood.Features.Carts.Contracts;
using PosTech.MyFood.Features.Carts.Entities;
using PosTech.MyFood.Features.Carts.Repositories;
using PosTech.MyFood.Features.Products.Entities;
using PosTech.MyFood.WebApi.Features.Carts.Entities;
using PosTech.MyFood.WebApi.Features.Carts.Services;
using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.Features.Carts.Services;

public class CartService(ICartRepository cartRepository) : ICartService
{
    public async Task<CartResponse> AddToCartAsync(string? customerId, CartItemDto cartItem)
    {
        var customer = customerId ?? Guid.NewGuid().ToString();
        var cart = await cartRepository.GetByCustomerIdAsync(customerId) ?? Cart.Create(CartId.New(), customer);

        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == new ProductId(cartItem.ProductId));
        if (existingItem != null)
        {
            existingItem.Quantity += cartItem.Quantity;
        }
        else
        {
            var newItem = CartItem.Create(
                CartItemId.New(),
                new ProductId(cartItem.ProductId),
                cartItem.ProductName,
                cartItem.UnitPrice,
                cartItem.Quantity,
                ProductCategory.Lanche); // Ajuste a categoria conforme necessário
            newItem.CartId = cart.Id;
            cart.AddItem(newItem);
        }

        if (await cartRepository.ExistsAsync(cart.Id))
            await cartRepository.UpdateAsync(cart);
        else
            await cartRepository.AddAsync(cart);

        return new CartResponse
        {
            Id = cart.Id.Value,
            CustomerId = cart.CustomerId,
            Items = cart.Items.Select(i => new CartItemDto
            {
                ProductId = i.ProductId.Value,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };
    }

    public async Task<CartResponse> GetCartByCustomerIdAsync(string customerId)
    {
        var cart = await cartRepository.GetByCustomerIdAsync(customerId);
        if (cart == null) return null;

        return new CartResponse
        {
            Id = cart.Id.Value,
            CustomerId = cart.CustomerId,
            Items = cart.Items.Select(i => new CartItemDto
            {
                ProductId = i.ProductId.Value,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };
    }

    public async Task<CartResponse> RemoveFromCartAsync(string customerId, Guid productId)
    {
        var cart = await cartRepository.GetByCustomerIdAsync(customerId);
        if (cart == null) return null;

        var item = cart.Items.Find(i => i.ProductId == new ProductId(productId));
        if (item != null)
        {
            cart.RemoveItem(item.Id);
            await cartRepository.UpdateAsync(cart);
        }

        return new CartResponse
        {
            Id = cart.Id.Value,
            CustomerId = cart.CustomerId,
            Items = cart.Items.Select(i => new CartItemDto
            {
                ProductId = i.ProductId.Value,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };
    }

    public async Task<CartResponse> ClearCartAsync(string customerId)
    {
        var cart = await cartRepository.GetByCustomerIdAsync(customerId);
        if (cart == null) return null;

        cart.Items.Clear();
        await cartRepository.UpdateAsync(cart);

        return new CartResponse
        {
            Id = cart.Id.Value,
            CustomerId = cart.CustomerId,
            Items = cart.Items.Select(i => new CartItemDto
            {
                ProductId = i.ProductId.Value,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };
    }
}