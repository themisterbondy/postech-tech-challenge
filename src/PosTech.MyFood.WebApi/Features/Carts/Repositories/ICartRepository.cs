using PosTech.MyFood.Features.Carts.Entities;
using PosTech.MyFood.WebApi.Features.Carts.Entities;

namespace PosTech.MyFood.Features.Carts.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetByCustomerIdAsync(string customerId);
    Task<bool> ExistsAsync(CartId cartId);
    Task AddAsync(Cart cart);
    Task UpdateAsync(Cart cart);
    Task DeleteCartsOlderThanAsync(DateTime threshold);
}