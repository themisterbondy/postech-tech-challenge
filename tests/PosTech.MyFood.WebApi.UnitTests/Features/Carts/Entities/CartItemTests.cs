using Xunit;
using FluentAssertions;
using NSubstitute;
using PosTech.MyFood.WebApi.Features.Carts.Entities;
using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.WebApi.Tests.Features.Carts.Entities
{
    public class CartItemTests
    {
        [Fact]
        public void Create_ShouldReturnCartItem_WhenGivenValidInputs()
        {
            // Arrange
            var cartItemId = CartItemId.New();
            var productId =ProductId.New();
            string productName = "Test Product";
            decimal unitPrice = 10.50m;
            int quantity = 5;
            ProductCategory category = ProductCategory.Lanche; // Categoria válida

            // Act
            CartItem result = CartItem.Create(cartItemId, productId, productName, unitPrice, quantity, category);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(cartItemId);
            result.ProductId.Should().Be(productId);
            result.ProductName.Should().Be(productName);
            result.UnitPrice.Should().Be(unitPrice);
            result.Quantity.Should().Be(quantity);
            result.Category.Should().Be(ProductCategory.Lanche); // Categoria deve ser Lanche
            result.CartId.Should().BeNull();
        }

        [Fact]
        public void Create_ShouldAssignCorrectCategory_WhenGivenDifferentCategories()
        {
            // Arrange
            var cartItemId = CartItemId.New();
            var productId =ProductId.New();
            string productName = "Test Product";
            decimal unitPrice = 10.50m;
            int quantity = 5;

            // Act & Assert - Para cada categoria
            CartItem lancheItem = CartItem.Create(cartItemId, productId, productName, unitPrice, quantity, ProductCategory.Lanche);
            lancheItem.Category.Should().Be(ProductCategory.Lanche);

            CartItem acompanhamentoItem = CartItem.Create(cartItemId, productId, productName, unitPrice, quantity, ProductCategory.Acompanhamento);
            acompanhamentoItem.Category.Should().Be(ProductCategory.Acompanhamento);

            CartItem bebidaItem = CartItem.Create(cartItemId, productId, productName, unitPrice, quantity, ProductCategory.Bebida);
            bebidaItem.Category.Should().Be(ProductCategory.Bebida);

            CartItem sobremesaItem = CartItem.Create(cartItemId, productId, productName, unitPrice, quantity, ProductCategory.Sobremesa);
            sobremesaItem.Category.Should().Be(ProductCategory.Sobremesa);
        }
    }
}