using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;
using SupplementsShop.ViewModels;

namespace SupplementsShop.Factories;

public interface IProductModelFactory
{
    public Product PrepareProductFromProductDto(ProductDto productDto);
    public ProductDto PrepareProductDto(Product? product);
    public IList<ProductDto>? PrepareProductDtos(IList<Product>? products);
    public ProductEditViewModel? PrepareProductEditViewModel(Product product);
    public Task<Product> PrepareProductFromProductEditViewModelAsync(ProductEditViewModel productEditViewModel);
}