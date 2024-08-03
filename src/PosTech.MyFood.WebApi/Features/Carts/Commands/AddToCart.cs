using FluentValidation;
using PosTech.MyFood.Features.Carts.Contracts;
using PosTech.MyFood.Features.Carts.Services;
using PosTech.MyFood.Features.Products.Repositories;
using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.Features.Carts.Commands;

public class AddToCart
{
    public class Command : IRequest<Result<CartResponse>>
    {
        public string CustomerCpf { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class AddToCartValidator : AbstractValidator<Command>
    {
        public AddToCartValidator()
        {
            RuleFor(x => x.CustomerCpf).NotEmpty().WithMessage("CustomerCpf is required.");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }

    public class Handler(ICartService cartService, IProductRepository productRepository)
        : IRequestHandler<Command, Result<CartResponse>>
    {
        public async Task<Result<CartResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var product = await productRepository.FindByIdAsync(new ProductId(request.ProductId), cancellationToken);
            if (product == null)
                return Result.Failure<CartResponse>(Error.NotFound("AddToCart.Handler",
                    $"Product with ID {request.ProductId} not found."));

            var cartItem = new CartItemDto
            {
                ProductId = product.Id.Value,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = request.Quantity
            };

            return await cartService.AddToCartAsync(request.CustomerCpf, cartItem);
        }
    }
}