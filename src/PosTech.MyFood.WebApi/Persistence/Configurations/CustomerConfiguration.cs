using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosTech.MyFood.WebApi.Features.Customers.Entities;

namespace PosTech.MyFood.WebApi.Persistence.Configurations;

[ExcludeFromCodeCoverage]
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("uuid_generate_v4()")
            .HasConversion(customerId => customerId.Value,
                value => new CustomerId(value));

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(l => l.Email).IsUnique();

        builder.Property(c => c.Cpf)
            .IsRequired()
            .HasMaxLength(11);

        builder.HasIndex(l => l.Cpf).IsUnique();

        builder.HasData(
            Customer.Create(new CustomerId(Guid.Parse("404912ea-3558-4a6c-8318-3c433f0a4459")), "John Doe", "john.doe@email.com", "36697999071")
        );
    }
}