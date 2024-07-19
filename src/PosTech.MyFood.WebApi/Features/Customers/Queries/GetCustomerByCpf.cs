using FluentValidation;
using PosTech.MyFood.WebApi.Common.Validation;
using PosTech.MyFood.WebApi.Features.Customers.Contracts;
using PosTech.MyFood.WebApi.Features.Customers.Repositories;

namespace PosTech.MyFood.Features.Customers.Queries;

public class GetCustomerByCpf
{
    public class Query : IRequest<Result<CustomerResponse>>
    {
        public string CPF { get; set; }
    }

    public class CreateCustomerValidator : AbstractValidator<Query>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.CPF)
                .NotEmpty().WithError(Error.Validation("CPF", "CPF is required."))
                .Matches("^[0-9]*$").WithError(Error.Validation("CPF", "CPF must contain only numbers."))
                .Length(11).WithError(Error.Validation("CPF", "CPF must have 11 characters."))
                .Must(GlobalValidations.BeAValidCPF).WithMessage("CPF is invalid.");
        }
    }

    public class GetCustomerByCpfHandler(ICustomerRepository customerRepository)
        : IRequestHandler<Query, Result<CustomerResponse>>
    {
        public async Task<Result<CustomerResponse>> Handle(Query request,
            CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetByCPFAsync(request.CPF, cancellationToken);

            if (customer.IsFailure)
                return Result.Failure<CustomerResponse>(customer.Error);

            return new CustomerResponse
            {
                Id = customer.Value.Id.Value,
                Name = customer.Value.Name,
                Email = customer.Value.Email,
                CPF = customer.Value.CPF
            };
        }
    }
}