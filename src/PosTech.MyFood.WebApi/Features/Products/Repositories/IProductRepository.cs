using PosTech.MyFood.Features.Products.Entities;

namespace PosTech.MyFood.Features.Products.Repositories;

public interface IProductRepository
{
    Task<Product?> FindByIdAsync(ProductId id, CancellationToken cancellationToken);
    Task<List<Product?>> FindByCategoryAsync(ProductCategory? category, CancellationToken cancellationToken);
    Task<Product?> CreateAsync(Product? product, CancellationToken cancellationToken);
    Task<Product?> UpdateAsync(Product? product, CancellationToken cancellationToken);
    Task DeleteAsync(Product? product, CancellationToken cancellationToken);
}