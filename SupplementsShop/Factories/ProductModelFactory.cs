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

    public IList<ProductDto>? PrepareProductDtos(IList<Product>? products)
    {
        return products?.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            Slug = p.Slug,
            Description = p.Description,
            Sales = p.Sales,
        }).ToList();
    }
}