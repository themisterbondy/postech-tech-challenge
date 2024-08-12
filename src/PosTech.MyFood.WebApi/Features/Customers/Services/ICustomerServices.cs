namespace PosTech.MyFood.WebApi.Features.Customers.Services;

public interface ICustomerServices
{
    Task<Result> IsUniqueCustomer(string? email, string? cpf, CancellationToken cancellationToken);
}