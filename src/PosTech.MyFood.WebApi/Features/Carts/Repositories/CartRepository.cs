using Microsoft.EntityFrameworkCore;
using PosTech.MyFood.Features.Carts.Entities;
using PosTech.MyFood.Features.Carts.Repositories;
using PosTech.MyFood.WebApi.Features.Carts.Entities;

namespace PosTech.MyFood.WebApi.Persistence.Repositories;

public class CartRepository(ApplicationDbContext context) : ICartRepository
{
    public async Task<Cart?> GetByCustomerIdAsync(string customerId)
    {
        return await context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);
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

    public async Task DeleteCartsOlderThanAsync(DateTime threshold)
    {
        var cartsToDelete = await context.Carts
            .Where(c => c.CreatedAt < threshold)
            .ToListAsync();

        context.Carts.RemoveRange(cartsToDelete);
        await context.SaveChangesAsync();
    }
}