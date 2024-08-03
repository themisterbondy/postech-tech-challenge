using PosTech.MyFood.Features.Carts.Contracts;

namespace PosTech.MyFood.WebApi.Features.Carts.Services;

public interface ICartService
{
    Task<CartResponse> AddToCartAsync(string customerCpf, CartItemDto cartItem);
    Task<CartResponse> GetCartByCustomerCpfAsync(string customerCpf);
    Task<CartResponse> RemoveFromCartAsync(string customerCpf, Guid productId);
    Task<CartResponse> ClearCartAsync(string customerCpf);
}