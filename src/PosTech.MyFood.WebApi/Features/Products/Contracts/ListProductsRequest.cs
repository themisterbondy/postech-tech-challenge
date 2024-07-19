using PosTech.MyFood.Features.Products.Entities;

namespace PosTech.MyFood.WebApi.Features.Products.Contracts;

public class ListProductsRequest
{
    public ProductCategory? Category { get; set; }
}