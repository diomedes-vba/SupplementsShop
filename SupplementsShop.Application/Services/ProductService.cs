using SupplementsShop.Domain.Interfaces;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IInventoryApiClient _inventoryApiClient;

    public ProductService(IProductRepository productRepository, IInventoryApiClient inventoryApiClient)
    {
        _productRepository = productRepository;
        _inventoryApiClient = inventoryApiClient;
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

    public async Task<int> GetProductQuantityAsync(string productNumber)
    {
        var inventoryItem = await _inventoryApiClient.GetInventoryItemAsync(productNumber);
        return inventoryItem.Quantity;
    }

    public async Task<Dictionary<string,int>> GetProductQuantityAsync(string[] productNumbers)
    {
        var items = await _inventoryApiClient.GetBatchInventoryItemsAsync(productNumbers);
        
        var lookup = items.ToDictionary(i => i.ProductNumber, i => i.Quantity);
        return lookup;
    }
}