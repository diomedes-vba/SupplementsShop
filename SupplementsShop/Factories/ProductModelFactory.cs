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
}