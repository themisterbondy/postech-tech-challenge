using PosTech.MyFood.WebApi.Features.Products.Entities;

namespace PosTech.MyFood.WebApi.Features.Products.Contracts;

public class ProductRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public ProductCategory Category { get; set; }
    public string? ImageUrl { get; set; }
}