using PosTech.MyFood.Features.Products.Entities;

namespace PosTech.MyFood.WebApi.UnitTests.Features.Orders.Entities;

public class ProductCategoryTests
{
    [Fact]
    public void ProductCategory_ShouldContainAllDefinedCategories()
    {
        var categories = Enum.GetValues(typeof(ProductCategory)).Cast<ProductCategory>().ToList();

        categories.Should().Contain(ProductCategory.Lanche);
        categories.Should().Contain(ProductCategory.Acompanhamento);
        categories.Should().Contain(ProductCategory.Bebida);
        categories.Should().Contain(ProductCategory.Sobremesa);
    }

    [Fact]
    public void ProductCategory_ShouldHaveCorrectNumberOfCategories()
    {
        var categories = Enum.GetValues(typeof(ProductCategory)).Cast<ProductCategory>().ToList();

        categories.Should().HaveCount(4);
    }
}