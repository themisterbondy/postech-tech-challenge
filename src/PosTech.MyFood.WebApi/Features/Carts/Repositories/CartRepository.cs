using Microsoft.EntityFrameworkCore;
using PosTech.MyFood.WebApi.Features.Carts.Entities;
using PosTech.MyFood.WebApi.Features.Payments.Emun;
using PosTech.MyFood.WebApi.Persistence;

namespace PosTech.MyFood.WebApi.Features.Carts.Repositories;

public class CartRepository(ApplicationDbContext context) : ICartRepository
{
    public async Task<Cart?> GetByCustomerIdAsync(string customerId)
    {
        return await context.Carts
            .AsNoTracking()
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }

    public async Task<Cart?> GetByIdAsync(CartId cartId)
    {
        return await context.Carts
            .AsNoTracking()
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == cartId);
    }

    public async Task<bool> ExistsAsync(CartId cartId)
    {
        return await context.Carts.AnyAsync(c => c.Id == cartId);
    }

    public async Task AddAsync(Cart cart)
    {
        await context.Carts.AddAsync(cart);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Cart cart)
    {
        context.Carts.Update(cart);
        await context.SaveChangesAsync();
    }

    public async Task DeleteUnpaidCartsOlderThanAsync(DateTime threshold)
    {
        var cartsToDelete = await context.Carts
            .Where(c => c.CreatedAt < threshold
                        && (c.PaymentStatus == PaymentStatus.NotStarted || c.PaymentStatus == PaymentStatus.Pending))
            .ToListAsync();

        context.Carts.RemoveRange(cartsToDelete);
        await context.SaveChangesAsync();
    }

    public Task<Cart?> GetByTransactionIdAsync(string transactionId)
    {
        return context.Carts
            .AsNoTracking()
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.TransactionId == transactionId);
    }
}