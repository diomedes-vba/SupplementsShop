using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetBySlugAsync (string slug);
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}