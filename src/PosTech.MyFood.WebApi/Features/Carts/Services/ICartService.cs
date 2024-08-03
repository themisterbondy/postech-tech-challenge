using PosTech.MyFood.Features.Carts.Contracts;

namespace PosTech.MyFood.WebApi.Features.Carts.Services;

public interface ICartService
{
    Task<CartResponse> AddToCartAsync(string? customerId, CartItemDto cartItem);
    Task<CartResponse> GetCartByCustomerIdAsync(string customerId);
    Task<CartResponse> RemoveFromCartAsync(string customerId, Guid productId);
    Task<CartResponse> ClearCartAsync(string customerId);
}