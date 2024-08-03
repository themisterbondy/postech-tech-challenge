using PosTech.MyFood.Features.Carts.Contracts;
using PosTech.MyFood.Features.Carts.Entities;
using PosTech.MyFood.Features.Products.Entities;
using PosTech.MyFood.WebApi.Features.Carts.Repositories;
using PosTech.MyFood.WebApi.Features.Carts.Services;
using PosTech.MyFood.WebApi.Features.Products.Entities;

public class CartService(ICartRepository cartRepository) : ICartService
{
    public async Task<CartResponse> AddToCartAsync(string customerCpf, CartItemDto cartItem)
    {
        var cart = await cartRepository.GetByCustomerCpfAsync(customerCpf);
        if (cart == null)
            cart = Cart.Create(CartId.New(), customerCpf);

        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == new ProductId(cartItem.ProductId));
        if (existingItem != null)
        {
            existingItem.Quantity += cartItem.Quantity;
        }
        else
        {
            var newItem = CartItem.Create(
                CartItemId.New(),
                new ProductId(cartItem.ProductId),
                cartItem.ProductName,
                cartItem.UnitPrice,
                cartItem.Quantity,
                ProductCategory.Lanche); // Ajuste a categoria conforme necessário
            newItem.CartId = cart.Id;
            cart.AddItem(newItem);
        }

        if (await cartRepository.ExistsAsync(cart.Id))
            await cartRepository.UpdateAsync(cart);
        else
            await cartRepository.AddAsync(cart);

        return new CartResponse
        {
            Id = cart.Id.Value,
            CustomerCpf = cart.CustomerCpf,
            Items = cart.Items.Select(i => new CartItemDto
            {
                ProductId = i.ProductId.Value,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };
    }

    public async Task<CartResponse> GetCartByCustomerCpfAsync(string customerCpf)
    {
        var cart = await cartRepository.GetByCustomerCpfAsync(customerCpf);
        if (cart == null) return null;

        return new CartResponse
        {
            Id = cart.Id.Value,
            CustomerCpf = cart.CustomerCpf,
            Items = cart.Items.Select(i => new CartItemDto
            {
                ProductId = i.ProductId.Value,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };
    }

    public async Task<CartResponse> RemoveFromCartAsync(string customerCpf, Guid productId)
    {
        var cart = await cartRepository.GetByCustomerCpfAsync(customerCpf);
        if (cart == null) return null;

        var item = cart.Items.Find(i => i.ProductId == new ProductId(productId));
        if (item != null)
        {
            cart.RemoveItem(item.Id);
            await cartRepository.UpdateAsync(cart);
        }

        return new CartResponse
        {
            Id = cart.Id.Value,
            CustomerCpf = cart.CustomerCpf,
            Items = cart.Items.Select(i => new CartItemDto
            {
                ProductId = i.ProductId.Value,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };
    }

    public async Task<CartResponse> ClearCartAsync(string customerCpf)
    {
        var cart = await cartRepository.GetByCustomerCpfAsync(customerCpf);
        if (cart == null) return null;

        cart.Items.Clear();
        await cartRepository.UpdateAsync(cart);

        return new CartResponse
        {
            Id = cart.Id.Value,
            CustomerCpf = cart.CustomerCpf,
            Items = cart.Items.Select(i => new CartItemDto
            {
                ProductId = i.ProductId.Value,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };
    }
}