using SupplementsShop.Application.DTOs;
using SupplementsShop.Application.Services;
using SupplementsShop.Domain.Entities;
using SupplementsShop.ViewModels;

namespace SupplementsShop.Factories;

public class ProductModelFactory : IProductModelFactory
{
    private readonly IProductService _productService;

    public ProductModelFactory(IProductService productService)
    {
        _productService = productService;
    }
    public ProductViewModel PrepareProductViewModel(Product? p, int quantity = 0)
    {
        return new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            Slug = p.Slug,
            Description = p.Description,
            Sales = p.Sales,
            CompanyId = p.CompanyId,
            Quantity = quantity
        };
    }

    public IList<ProductViewModel>? PrepareProductViewModels(IList<Product>? products)
    {
        return products?.Select(PrepareProductViewModel).ToList();
    }

    public ProductEditViewModel PrepareProductEditViewModel(Product? product)
    {
        return new ProductEditViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl
        };
    }

    public async Task<Product> PrepareProductFromProductEditViewModelAsync(ProductEditViewModel? productEditViewModel)
    {
        var oldProduct = await _productService.GetProductByIdAsync(productEditViewModel.Id);
        return new Product(
            id: productEditViewModel.Id, 
            name: productEditViewModel.Name, 
            productNumber: oldProduct.ProductNumber,
            price: oldProduct.Price,
            description: productEditViewModel.Description,
            companyId: oldProduct.CompanyId,
            slug: oldProduct.Slug,
            imageUrl: productEditViewModel.ImageUrl);
    }
}