using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Application.Services;

public interface ICategoryService
{
    Task<Category?> GetCategoryBySlugAsync(string slug);
    Task<Category?> GetCategoryByIdAsync(int categoryId);
    Task<IList<Category>> GetAllCategoriesAsync();
    int GetProductCountForCategory(int categoryId);
}