using SupplementsShop.Domain.Interfaces;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<ProductDto?> GetProductBySlugAsync(string slug)
    {
        var product = await _productRepository.GetBySlugAsync(slug);

        if (product == null) return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Slug = product.Slug,
            Sales = product.Sales,
        };
    }
    
    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null) return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Slug = product.Slug,
            Sales = product.Sales,
        };
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Quantity = p.Quantity,
            Description = p.Description,
            ImageUrl = p.ImageUrl,
            Slug = p.Slug,
            Sales = p.Sales,
        });
    }

    public async Task<IPagedList<Product>?> GetProductListByCategoryIdAsync(int categoryId, int page = 0, int pageSize = int.MaxValue)
    {
        var pagedList = await _productRepository.GetByCategoryIdAsync(categoryId, page, pageSize);
        return pagedList;
    }
}