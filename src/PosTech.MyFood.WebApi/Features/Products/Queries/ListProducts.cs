using PosTech.MyFood.Features.Products.Entities;
using PosTech.MyFood.Features.Products.Repositories;
using PosTech.MyFood.WebApi.Features.Products.Contracts;

namespace PosTech.MyFood.Features.Products.Queries;

public class ListProducts
{
    public class Query : IRequest<Result<ListProductsResponse>>
    {
        public ProductCategory? Category { get; set; }
    }

    public class ListProductsHandler(IProductRepository productRepository)
        : IRequestHandler<Query, Result<ListProductsResponse>>
    {
        public async Task<Result<ListProductsResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var products = await productRepository.FindByCategoryAsync(request.Category, cancellationToken);

            return new ListProductsResponse
            {
                Products = products.Select(x => new ProductResponse
                {
                    Id = x.Id.Value,
                    Name = x.Name,
                    Description = x.Description,
                    Category = x.Category,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl
                }).ToList()
            };
        }
    }
}