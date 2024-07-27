using Microsoft.AspNetCore.Mvc;
using PosTech.MyFood.Features.Products.Commands;
using PosTech.MyFood.Features.Products.Entities;
using PosTech.MyFood.Features.Products.Queries;
using PosTech.MyFood.WebApi.Features.Products.Contracts;

namespace PosTech.MyFood.Features.Products.Endpoints;

public class ProductsEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products");

        group.MapGet("/category", async ([FromQuery] ProductCategory request, [FromServices] IMediator mediator) =>
            {
                var query = new ListProducts.Query
                {
                    Category = request
                };

                var result = await mediator.Send(query);

                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : result.ToProblemDetails();
            })
            .WithName("ListProducts")
            .Produces<ListProductsResponse>(200)
            .WithTags("Products")
            .WithOpenApi();

        group.MapPost("/", async ([FromBody] ProductRequest request, [FromServices] IMediator mediator) =>
            {
                var command = new CreateProduct.Command
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Category = request.Category,
                    ImageUrl = request.ImageUrl
                };

                var result = await mediator.Send(command);

                return result.IsSuccess
                    ? Results.Created($"/Products/{result.Value.Id}", result.Value)
                    : result.ToProblemDetails();
            })
            .WithName("CreateProduct")
            .Accepts<ProductRequest>("application/json")
            .Produces<ProductResponse>(201)
            .WithTags("Products")
            .WithOpenApi();

        group.MapPut("/{id}",
                async ([FromQuery] Guid id, [FromBody] ProductRequest request, [FromServices] IMediator mediator) =>
                {
                    var command = new UpdateProduct.Command
                    {
                        Id = id,
                        Name = request.Name,
                        Description = request.Description,
                        Price = request.Price,
                        Category = request.Category,
                        ImageUrl = request.ImageUrl
                    };

                    var result = await mediator.Send(command);

                    return result.IsSuccess
                        ? Results.Ok(result.Value)
                        : result.ToProblemDetails();
                })
            .WithName("UpdateProduct")
            .Accepts<ProductRequest>("application/json")
            .Produces<ProductResponse>(200)
            .WithTags("Products")
            .WithOpenApi();

        group.MapDelete("/{id}", async ([FromQuery] Guid id, [FromServices] IMediator mediator) =>
            {
                var command = new DeleteProduct.Command
                {
                    Id = id
                };

                var result = await mediator.Send(command);

                return result.IsSuccess
                    ? Results.NoContent()
                    : result.ToProblemDetails();
            })
            .WithName("DeleteProduct")
            .Produces(204)
            .WithTags("Products")
            .WithOpenApi();
    }
}