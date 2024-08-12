using FluentValidation;
using PosTech.MyFood.WebApi.Common.Validation;
using PosTech.MyFood.WebApi.Features.Customers.Contracts;
using PosTech.MyFood.WebApi.Features.Customers.Entities;
using PosTech.MyFood.WebApi.Features.Customers.Repositories;
using PosTech.MyFood.WebApi.Features.Customers.Services;

namespace PosTech.MyFood.WebApi.Features.Customers.Commands;

public class CreateCustomer
{
    public class Command : IRequest<Result<CustomerResponse>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
    }

    public class CreateCustomerValidator : AbstractValidator<Command>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithError(Error.Validation("Name", "Name is required."));

            RuleFor(x => x.Email)
                .NotEmpty().WithError(Error.Validation("Email", "Email is required."))
                .EmailAddress().WithError(Error.Validation("Email", "Email is invalid."));

            RuleFor(x => x.CPF)
                .NotEmpty().WithError(Error.Validation("CPF", "CPF is required."))
                .Matches("^[0-9]*$").WithError(Error.Validation("CPF", "CPF must contain only numbers."))
                .Length(11).WithError(Error.Validation("CPF", "CPF must have 11 characters."))
                .Must(GlobalValidations.BeAValidCPF).WithMessage("CPF is invalid.");
        }
    }

    public class CreateCustomerHandler(ICustomerRepository customerRepository, ICustomerServices customerServices)
        : IRequestHandler<Command, Result<CustomerResponse>>
    {
        public async Task<Result<CustomerResponse>> Handle(Command request,
            CancellationToken cancellationToken)
        {
            var isUniqueCustomer =
                await customerServices.IsUniqueCustomer(request.Email, request.CPF, cancellationToken);

            if (isUniqueCustomer.IsFailure)
                return Result.Failure<CustomerResponse>(isUniqueCustomer.Error);

            var createCustomer = await customerRepository.CreateAsync(
                Customer.Create(CustomerId.New(),
                    request.Name,
                    request.Email,
                    request.CPF),
                cancellationToken);

            if (createCustomer.IsFailure)
                return Result.Failure<CustomerResponse>(createCustomer.Error);

            return new CustomerResponse
            {
                Id = createCustomer.Value.Id.Value,
                Name = createCustomer.Value.Name,
                Email = createCustomer.Value.Email,
                CPF = createCustomer.Value.CPF
            };
        }
    }
}