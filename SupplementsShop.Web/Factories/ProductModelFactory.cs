using SupplementsShop.Web.ViewModels;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Application.Services;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Web.Factories;

public class ProductModelFactory : IProductModelFactory
{
    private readonly IProductService _productService;

    public ProductModelFactory(IProductService productService)
    {
        _productService = productService;
    }
    public ProductDetailsViewModel PrepareProductDetailsViewModel(Product? p, int? quantity = null)
    {
        return new ProductDetailsViewModel
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

    public ProductCategoryViewModel PrepareProductCategoryViewModel(Product? p)
    {
        return new ProductCategoryViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            Slug = p.Slug
        };
    }
    
    public IList<ProductCategoryViewModel>? PrepareProductCategoryViewModels(IList<Product>? products)
    {
        return products?.Select(PrepareProductCategoryViewModel).ToList();
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
        oldProduct.EditNameDescriptionImage(productEditViewModel.Name, 
            productEditViewModel.Description, productEditViewModel.ImageUrl);
        
        return oldProduct;
    }
}