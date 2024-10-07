using Microsoft.AspNetCore.Mvc;
using PosTech.MyFood.WebApi.Features.Customers.Commands;
using PosTech.MyFood.WebApi.Features.Customers.Contracts;
using PosTech.MyFood.WebApi.Features.Customers.Queries;

namespace PosTech.MyFood.WebApi.Features.Customers.Endpoints;

[ExcludeFromCodeCoverage]
public class CustomersEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customers");

        group.MapPost("/", async (CustomerRequest request, [FromServices] IMediator mediator) =>
            {
                var command = new CreateCustomer.Command
                {
                    Name = request.Name,
                    Email = request.Email,
                    Cpf = request.Cpf
                };

                var result = await mediator.Send(command);

                return result.IsSuccess
                    ? Results.Created($"/Customers/{result.Value.Cpf}", result.Value)
                    : result.ToProblemDetails();
            })
            .WithName("CreateCustomer")
            .Accepts<CustomerRequest>("application/json")
            .Produces<CustomerResponse>(201)
            .WithTags("Customers")
            .WithOpenApi();

        group.MapGet("/{cpf}", async (string cpf, [FromServices] IMediator mediator) =>
            {
                var query = new GetCustomerByCpf.Query { Cpf = cpf };
                var result = await mediator.Send(query);
                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : result.ToProblemDetails();
            })
            .WithName("GetCustomerByCpf")
            .Produces<CustomerResponse>()
            .WithTags("Customers")
            .WithOpenApi();
    }
}