using Xunit;
using FluentAssertions;
using NSubstitute;
using PosTech.MyFood.WebApi.Features.Carts.Entities;
using PosTech.MyFood.WebApi.Features.Products.Entities;
using PosTech.MyFood.WebApi.Features.Payments.Emun;
using System;
using System.Collections.Generic;

namespace PosTech.MyFood.WebApi.Tests.Features.Carts.Entities
{
    public class CartTests
    {
        [Fact]
        public void Create_ShouldInitializeCart_WhenGivenValidInputs()
        {
            // Arrange
            var cartId = CartId.New();
            string customerId = "customer123";

            // Act
            Cart cart = Cart.Create(cartId, customerId);

            // Assert
            cart.Should().NotBeNull();
            cart.Id.Should().Be(cartId);
            cart.CustomerId.Should().Be(customerId);
            cart.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1)); // Verifica se a data de criação está correta
            cart.Items.Should().BeEmpty(); // O carrinho deve ser criado vazio
            cart.PaymentStatus.Should().Be(PaymentStatus.NotStarted);
        }

        [Fact]
        public void AddItem_ShouldAddItemToCart_WhenValidItemProvided()
        {
            // Arrange
            var cartId = CartId.New();
            var cart = Cart.Create(cartId, "customer123");
            var cartItem = CartItem.Create(CartItemId.New(), ProductId.New(), "Product 1", 10.99m, 1, ProductCategory.Lanche);

            // Act
            cart.AddItem(cartItem);

            // Assert
            cart.Items.Should().ContainSingle().Which.Should().Be(cartItem);
        }

        [Fact]
        public void RemoveItem_ShouldRemoveItemFromCart_WhenItemExists()
        {
            // Arrange
            var cartId = CartId.New();
            var cart = Cart.Create(cartId, "customer123");
            var cartItem = CartItem.Create(CartItemId.New(), ProductId.New(), "Product 1", 10.99m, 1, ProductCategory.Lanche);
            cart.AddItem(cartItem);

            // Act
            cart.RemoveItem(cartItem.Id);

            // Assert
            cart.Items.Should().BeEmpty();
        }

        [Fact]
        public void RemoveItem_ShouldDoNothing_WhenItemDoesNotExist()
        {
            // Arrange
            var cartId = CartId.New();
            var cart = Cart.Create(cartId, "customer123");
            var itemId = CartItemId.New();

            // Act
            cart.RemoveItem(itemId);

            // Assert
            cart.Items.Should().BeEmpty(); // Carrinho permanece vazio
        }

        [Fact]
        public void UpdatePaymentStatus_ShouldChangePaymentStatus()
        {
            // Arrange
            var cartId = CartId.New();
            var cart = Cart.Create(cartId, "customer123");

            // Act
            cart.UpdatePaymentStatus(PaymentStatus.Accepted);

            // Assert
            cart.PaymentStatus.Should().Be(PaymentStatus.Accepted);
        }
    }
}