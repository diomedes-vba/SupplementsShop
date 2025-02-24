using SupplementsShop.Application.DTOs;

namespace SupplementsShop.Application.Services;

public interface ICategoryService
{
    Task<CategoryDto> GetCategoryBySlugAsync(string slug);
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
}