using PosTech.MyFood.Features.Carts.Entities;

namespace PosTech.MyFood.WebApi.Features.Carts.Repositories;

public interface ICartRepository
{
    Task<Cart> GetByCustomerCpfAsync(string customerCpf);
    Task<bool> ExistsAsync(CartId cartId);
    Task AddAsync(Cart cart);
    Task UpdateAsync(Cart cart);
}