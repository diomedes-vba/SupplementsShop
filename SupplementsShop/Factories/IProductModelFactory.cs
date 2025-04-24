using SupplementsShop.ViewModels;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Factories;

public interface IProductModelFactory
{
    public ProductDetailsViewModel PrepareProductDetailsViewModel(Product? product, int? quantity = null);
    public ProductCategoryViewModel PrepareProductCategoryViewModel(Product? product);
    public IList<ProductCategoryViewModel>? PrepareProductCategoryViewModels(IList<Product>? products);
    public ProductEditViewModel? PrepareProductEditViewModel(Product product);
    public Task<Product> PrepareProductFromProductEditViewModelAsync(ProductEditViewModel productEditViewModel);
}