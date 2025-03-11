using SupplementsShop.Application.DTOs;

namespace SupplementsShop.Application.Services;

public interface ICategoryService
{
    Task<CategoryDto?> GetCategoryBySlugAsync(string slug, int pageNumber = 1);
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
}