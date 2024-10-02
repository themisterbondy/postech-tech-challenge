using PosTech.MyFood.WebApi.Features.Carts.Entities;

namespace PosTech.MyFood.WebApi.Features.Carts.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetByCustomerIdAsync(string customerId);
    Task<Cart?> GetByIdAsync(CartId cartId);
    Task<bool> ExistsAsync(CartId cartId);
    Task AddAsync(Cart cart);
    Task UpdateStatusAsync(Cart cart);
    Task UpdateAsync(Cart cart);
    Task DeleteUnpaidCartsOlderThanAsync(DateTime threshold);
    Task<Cart?> GetByTransactionIdAsync(string transactionId);
    Task Delete(Cart cart);
}