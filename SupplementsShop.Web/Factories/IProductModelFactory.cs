using SupplementsShop.Web.ViewModels;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Web.Factories;

public interface IProductModelFactory
{
    ProductDetailsViewModel PrepareProductDetailsViewModel(Product? product, int? quantity = null);
    ProductCategoryViewModel PrepareProductCategoryViewModel(Product? product);
    IList<ProductCategoryViewModel>? PrepareProductCategoryViewModels(IList<Product>? products);
    ProductEditViewModel? PrepareProductEditViewModel(Product product);
    Task<Product> PrepareProductFromProductEditViewModelAsync(ProductEditViewModel productEditViewModel);
}