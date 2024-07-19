using Microsoft.EntityFrameworkCore;
using PosTech.MyFood.WebApi.Features.Customers.Entities;
using PosTech.MyFood.WebApi.Persistence;

namespace PosTech.MyFood.WebApi.Features.Customers.Repositories;

public class CustomerRepository(ApplicationDbContext _context) : ICustomerRepository
{
    public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return customer;
    }

    public async Task<Customer> GetByCPFAsync(string cpf, CancellationToken cancellationToken)
    {
        return
            await _context.Customers.FirstOrDefaultAsync(x => x.CPF == cpf, cancellationToken);
    }

    public async Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return
            await _context.Customers.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}