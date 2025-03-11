using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetBySlugAsync(string slug, int page, int pageSize = 5);
    Task<List<Category>> GetAllAsync();
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(int id);
}