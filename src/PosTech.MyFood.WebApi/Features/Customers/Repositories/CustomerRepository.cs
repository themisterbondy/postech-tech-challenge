using Microsoft.EntityFrameworkCore;
using PosTech.MyFood.WebApi.Features.Customers.Entities;
using PosTech.MyFood.WebApi.Persistence;

namespace PosTech.MyFood.WebApi.Features.Customers.Repositories;

public class CustomerRepository(ApplicationDbContext _context) : ICustomerRepository
{
    public async Task<Result<Customer>> CreateAsync(Customer? customer, CancellationToken cancellationToken)
    {
        if (customer == null)
            return Result.Failure<Customer>(Error.Failure("CustomerRepository.CreateAsync", "Customer is null."));

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return customer;
    }

    public async Task<Result<Customer>?> GetByCPFAsync(string? cpf, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(cpf))
            return Result.Failure<Customer>(Error.Failure("CustomerRepository.GetByCPFAsync", "CPF is null."));

        return
            await _context.Customers.FirstOrDefaultAsync(x => x.CPF == cpf, cancellationToken);
    }

    public async Task<Result<Customer>?> GetByEmailAsync(string? email, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(email))
            return Result.Failure<Customer>(Error.Failure("CustomerRepository.GetByEmailAsync", "Email is null."));

        return
            await _context.Customers.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}