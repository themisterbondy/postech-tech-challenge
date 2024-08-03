using Microsoft.EntityFrameworkCore;
using PosTech.MyFood.Features.Carts.Entities;
using PosTech.MyFood.Features.Carts.Repositories;
using PosTech.MyFood.WebApi.Persistence;

namespace PosTech.MyFood.WebApi.Features.Carts.Repositories;

public class CartRepository(ApplicationDbContext context) : ICartRepository
{
    public async Task<Cart> GetByCustomerCpfAsync(string customerCpf)
    {
        return await context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerCpf == customerCpf);
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
}