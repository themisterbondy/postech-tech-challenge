using PosTech.MyFood.Features.Carts.Entities;

namespace PosTech.MyFood.Features.Carts.Repositories;

public interface ICartRepository
{
    Task<Cart> GetByCustomerCpfAsync(string customerCpf);
    Task AddAsync(Cart cart);
    Task UpdateAsync(Cart cart);
}