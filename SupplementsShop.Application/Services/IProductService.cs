using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Application.Services;

public interface IProductService
{
    Task UpdateProduct(Product product);
    Task<Product?> GetProductBySlugAsync(string slug);
    Task<Product?> GetProductByIdAsync(int productId);
    Task<IEnumerable<Product>?> GetAllProductsAsync();
    Task<IPagedList<Product>?> GetProductListByCategoryIdAsync(int categoryId, int page = 0, int pageSize = int.MaxValue);
    Task<int?> GetProductQuantityAsync(string productNumber);
    Task<Dictionary<string, int>?> GetProductQuantityDictAsync(string[] productNumbers);
    Task<IPagedList<Product>?> SearchProductsByStringAsync(string searchTerm, int page = 0, int pageSize = int.MaxValue);
}