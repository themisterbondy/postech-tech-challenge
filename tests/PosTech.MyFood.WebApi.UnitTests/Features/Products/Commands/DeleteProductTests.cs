using PosTech.MyFood.Features.Products.Entities;
using PosTech.MyFood.Features.Products.Repositories;
using PosTech.MyFood.WebApi.Features.Products.Commands;
using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Products.Commands;

public class DeleteProductTests
{
    private readonly DeleteProduct.DeleteProductHandler _handler;
    private readonly IProductRepository _productRepository;
    private readonly DeleteProduct.DeleteProductValidator _validator;

    public DeleteProductTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new DeleteProduct.DeleteProductHandler(_productRepository);
        _validator = new DeleteProduct.DeleteProductValidator();
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenIdIsEmpty()
    {
        var command = new DeleteProduct.Command { Id = Guid.Empty };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Validator_ShouldNotHaveError_WhenIdIsNotEmpty()
    {
        var command = new DeleteProduct.Command { Id = Guid.NewGuid() };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenProductIsDeleted()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = Product.Create(ProductId.New(), "Test Product", null, 10, ProductCategory.Acompanhamento, null);
        _productRepository.FindByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>()).Returns(product);

        var command = new DeleteProduct.Command { Id = productId };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(productId);
        await _productRepository.Received(1).DeleteAsync(product, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenProductNotFound()
    {
        // Arrange
        _productRepository.FindByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>()).Returns((Product)null);

        var command = new DeleteProduct.Command { Id = Guid.NewGuid() };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be("DeleteProductHandler.Handle");
        await _productRepository.DidNotReceive().DeleteAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }
}