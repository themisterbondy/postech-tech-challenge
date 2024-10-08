using PosTech.MyFood.WebApi.Features.Carts.Commands;
using PosTech.MyFood.WebApi.Features.Carts.Contracts;

namespace PosTech.MyFood.WebApi.Features.Carts.Endpoints;

[ExcludeFromCodeCoverage]
public class CartEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/carts");

        group.MapPost("/", async (CartRequest request, ISender sender) =>
            {
                var Command = new AddToCart.Command
                {
                    CustomerId = request.CustomerId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };

                var result = await sender.Send(Command);

                return result.IsSuccess
                    ? Results.Created($"/Carts/{result.Value.Id}", result.Value)
                    : result.ToProblemDetails();
            })
            .WithName("AddToCart")
            .Accepts<CartRequest>("application/json")
            .Produces<CartResponse>(201)
            .WithTags("Carts")
            .WithOpenApi();
    }
}