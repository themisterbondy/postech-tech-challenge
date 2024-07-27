using Microsoft.EntityFrameworkCore;
using PosTech.MyFood.WebApi.Features.Customers.Entities;
using PosTech.MyFood.WebApi.Features.Orders.Entities;
using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.WebApi.Persistence;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderQueue> OrderQueue { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}