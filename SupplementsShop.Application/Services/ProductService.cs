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

    public async Task UpdateProduct(Product product)
    {
        var slug = GenerateSlugFromName(product.Name);
        product.UpdateSlug(slug);
        await _productRepository.UpdateAsync(product);
    }
    
    public async Task<Product?> GetProductBySlugAsync(string slug)
    {
        var product = await _productRepository.GetBySlugAsync(slug);

        if (product == null) return null;

        return product;
    }
    
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null) return null;

        return product;
    }

    public async Task<IEnumerable<Product>?> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<IPagedList<Product>?> GetProductListByCategoryIdAsync(int categoryId, int page = 0, int pageSize = int.MaxValue)
    {
        var pagedList = await _productRepository.GetByCategoryIdAsync(categoryId, page, pageSize);
        return pagedList;
    }

    private string GenerateSlugFromName(string name)
    {
        return name.ToLower().Replace(", ", "-").Replace(" ", "-").Replace(",", "-");
    }
}