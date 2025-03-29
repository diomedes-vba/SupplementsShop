using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetBySlugAsync(string slug);
    Task<Category?> GetByIdAsync(int id);
    Task<List<Category>> GetAllAsync();
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(int id);
}