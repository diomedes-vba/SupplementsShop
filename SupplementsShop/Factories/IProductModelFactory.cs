using SupplementsShop.ViewModels;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Factories;

public interface IProductModelFactory
{
    public ProductViewModel PrepareProductViewModel(Product? product, int quantity = 0);
    public IList<ProductViewModel>? PrepareProductViewModels(IList<Product>? products);
    public ProductEditViewModel? PrepareProductEditViewModel(Product product);
    public Task<Product> PrepareProductFromProductEditViewModelAsync(ProductEditViewModel productEditViewModel);
}