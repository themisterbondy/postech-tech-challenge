using PosTech.MyFood.WebApi.Features.Carts.Contracts;
using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.WebApi.Features.Carts.Services;

public interface ICartService
{
    Task<CartResponse> AddToCartAsync(string? customerId, CartItemDto cartItem, Product product);
    Task<CartResponse> GetCartByCustomerIdAsync(string customerId);
    Task<CartResponse> RemoveFromCartAsync(string customerId, Guid productId);
    Task<CartResponse> ClearCartAsync(Guid cartId);
}