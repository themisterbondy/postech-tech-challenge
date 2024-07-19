using Microsoft.EntityFrameworkCore;
using PosTech.MyFood.WebApi.Features.Customers.Entities;
using PosTech.MyFood.WebApi.Persistence;

namespace PosTech.MyFood.Features.Customers.Repository;

public class CustomerRepository(ApplicationDbContext _context) : ICustomerRepository
{
    public async Task<Result<Customer>> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(customer);
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