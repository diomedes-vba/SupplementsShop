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

    public Product PrepareProductFromProductDto(ProductDto productDto)
    {
        return new Product(
            id: productDto.Id, 
            name: productDto.Name, 
            price: productDto.Price,
            description: productDto.Description,
            quantity: productDto.Quantity,
            companyId: productDto.CompanyId,
            slug: productDto.Slug,
            imageUrl: productDto.ImageUrl);
    }

    public ProductDto PrepareProductDto(Product? p)
    {
        return new ProductDto
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

    public IList<ProductDto>? PrepareProductDtos(IList<Product>? products)
    {
        return products?.Select(PrepareProductDto).ToList();
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