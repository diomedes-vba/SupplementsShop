using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetBySlugAsync (string slug);
    Task<Product?> GetByIdAsync(int id);
    Task<IPagedList<Product>?> GetByCategoryIdAsync(int categoryId, int page, int pageSize = int.MaxValue);
    Task<IList<Product>?> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
    Task<IList<int>> GetCategoryIdsForProductAsync(int productId);
}