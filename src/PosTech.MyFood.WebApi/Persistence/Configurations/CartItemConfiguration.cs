using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosTech.MyFood.Features.Carts.Entities;
using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.Data.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .HasConversion(id => id.Value, value => new CartItemId(value))
            .IsRequired();

        builder.Property(ci => ci.ProductId)
            .HasConversion(id => id.Value, value => new ProductId(value))
            .IsRequired();

        builder.Property(ci => ci.ProductName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(ci => ci.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(ci => ci.Quantity)
            .IsRequired();

        builder.Property(ci => ci.CartId)
            .HasConversion(id => id.Value, value => new CartId(value))
            .IsRequired();

        builder.HasOne<Cart>()
            .WithMany(c => c.Items)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}