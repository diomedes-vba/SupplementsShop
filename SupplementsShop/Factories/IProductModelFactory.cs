using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;
using SupplementsShop.ViewModels;

namespace SupplementsShop.Factories;

public interface IProductModelFactory
{
    public ProductViewModel PrepareProductViewModel(Product? product);
    public IList<ProductViewModel>? PrepareProductViewModels(IList<Product>? products);
    public ProductEditViewModel? PrepareProductEditViewModel(Product product);
    public Task<Product> PrepareProductFromProductEditViewModelAsync(ProductEditViewModel productEditViewModel);
}