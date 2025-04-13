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

    public Product PrepareProductFromProductViewModel(ProductViewModel productModel)
    {
        return new Product(
            id: productModel.Id, 
            name: productModel.Name, 
            price: productModel.Price,
            description: productModel.Description,
            quantity: productModel.Quantity,
            companyId: productModel.CompanyId,
            slug: productModel.Slug,
            imageUrl: productModel.ImageUrl);
    }

    public ProductViewModel PrepareProductViewModel(Product? p)
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
            CompanyId = p.CompanyId
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
            price: oldProduct.Price,
            description: productEditViewModel.Description,
            quantity: oldProduct.Quantity,
            companyId: oldProduct.CompanyId,
            slug: oldProduct.Slug,
            imageUrl: productEditViewModel.ImageUrl);
    }
}