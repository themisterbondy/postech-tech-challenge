using FluentValidation;
using PosTech.MyFood.Features.Carts.Contracts;
using PosTech.MyFood.Features.Products.Repositories;
using PosTech.MyFood.WebApi.Common.Validation;
using PosTech.MyFood.WebApi.Features.Carts.Services;
using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.Features.Carts.Commands;

public class AddToCart
{
    public class Command : IRequest<Result<CartResponse>>
    {
        public string CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class AddToCartValidator : AbstractValidator<Command>
    {
        public AddToCartValidator()
        {
            When(x => !string.IsNullOrEmpty(x.CustomerId),
                () =>
                {
                    RuleFor(x => x.CustomerId)
                        .NotEmpty().WithError(Error.Validation("CPF", "CPF is required."))
                        .Matches("^[0-9]*$").WithError(Error.Validation("CPF", "CPF must contain only numbers."))
                        .Length(11).WithError(Error.Validation("CPF", "CPF must have 11 characters."))
                        .Must(GlobalValidations.BeAValidCPF).WithMessage("CPF is invalid.");
                });
            RuleFor(x => x.ProductId).NotEmpty().WithError(Error.Validation("ProductId", "ProductId is required."));
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }

    public class Handler : IRequestHandler<Command, Result<CartResponse>>
    {
        private readonly ICartService _cartService;
        private readonly IProductRepository _productRepository;

        public Handler(ICartService cartService, IProductRepository productRepository)
        {
            _cartService = cartService;
            _productRepository = productRepository;
        }

        public async Task<Result<CartResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindByIdAsync(new ProductId(request.ProductId), cancellationToken);
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

            return await _cartService.AddToCartAsync(request.CustomerId, cartItem);
        }
    }
}