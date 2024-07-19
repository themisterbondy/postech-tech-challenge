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

    public async Task<Result<Customer>> GetByCPFAsync(string cpf, CancellationToken cancellationToken)
    {
        var existingCustomer =
            await _context.Customers.FirstOrDefaultAsync(x => x.CPF == cpf, cancellationToken);

        return existingCustomer == null
            ? Result.Failure<Customer>(Error.NotFound("CustomerRepository.GetByCPFAsync", "Customer not found."))
            : Result.Success(existingCustomer);
    }
}