using Microsoft.Extensions.Caching.Memory;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Infrastructure.Persistence;

public class CachedProductRepository : IProductRepository
{
    private readonly IProductRepository _innerRepository;
    private readonly IMemoryCache _cache;
    private static readonly Dictionary<int, int> CategoryCacheVersions = new();

    public CachedProductRepository(IProductRepository productRepository, IMemoryCache cache)
    {
        _innerRepository = productRepository;
        _cache = cache;
    }

    private int GetCategoryVersion(int categoryId)
    {
        if (!CategoryCacheVersions.TryGetValue(categoryId, out var version))
        {
            version = 1;
            CategoryCacheVersions[categoryId] = version;
        }

        return version;
    }

    private void IncrementCategoryVersion(int categoryId)
    {
        if (CategoryCacheVersions.ContainsKey(categoryId))
        {
            CategoryCacheVersions[categoryId]++;
        }
        else
        {
            CategoryCacheVersions[categoryId] = 1;
        }
    }

    public async Task<Product?> GetBySlugAsync(string slug)
    {
        var cacheKey = $"Product_Slug_{slug}";
        if (!_cache.TryGetValue(cacheKey, out Product? product))
        {
            product  = await _innerRepository.GetBySlugAsync(slug);
            if (product != null)
            {
                _cache.Set(cacheKey, product, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10)));
            }
        }
        return product;
    }

    public async Task<Product?> GetByIdAsync(int productId)
    {
        var cacheKey = $"Product_Id_{productId}";
        if (!_cache.TryGetValue(cacheKey, out Product? product))
        {
            product = await _innerRepository.GetByIdAsync(productId);
            if (product != null)
            {
                _cache.Set(cacheKey, product, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10)));
            }
        }
        return product;
    }

    public async Task<IPagedList<Product>?> GetByCategoryIdAsync(int categoryId, int page, int pageSize = int.MaxValue)
    {
        var version = GetCategoryVersion(categoryId);
        var cacheKey = $"CategoryProducts_{categoryId}_{version}_{page}_{pageSize}";
        if (!_cache.TryGetValue(cacheKey, out IPagedList<Product>? pagedProducts))
        {
            pagedProducts = await _innerRepository.GetByCategoryIdAsync(categoryId, page, pageSize);
            if (pagedProducts != null)
            {
                _cache.Set(cacheKey, pagedProducts, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10)));
            }
        }
        return pagedProducts;
    }

    public async Task<IList<Product>?> GetAllAsync()
    {
        var cacheKey = "Products_All";
        if (!_cache.TryGetValue(cacheKey, out IList<Product>? products))
        {
            products = await _innerRepository.GetAllAsync();
            {
                _cache.Set(cacheKey, products, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10)));
            }
        }
        return products;
    }

    public async Task AddAsync(Product product)
    {
        await _innerRepository.AddAsync(product);
        _cache.Remove("Products_All");
        // Increment Category Version when I figure out CUD functions
    }

    public async Task UpdateAsync(Product product)
    {
        await _innerRepository.UpdateAsync(product);
        _cache.Remove($"Product_Id_{product.Id}");
        _cache.Remove($"Product_Slug_{product.Slug}");
        _cache.Remove("Products_All");
        
        var categoryIds = await GetCategoryIdsForProductAsync(product.Id);
        foreach (var categoryId in categoryIds)
        {
            IncrementCategoryVersion(categoryId);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _innerRepository.DeleteAsync(id);
        _cache.Remove($"Product_Id_{id}");
        // Make a remove function for slug
        _cache.Remove("Products_All");
        // Increment Category Version when I figure out CUD functions
    }

    public async Task<IList<int>> GetCategoryIdsForProductAsync(int productId)
    {
        return await _innerRepository.GetCategoryIdsForProductAsync(productId);
    }
}