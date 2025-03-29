using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category?> GetCategoryBySlugAsync(string slug)
    {
        var category = await _categoryRepository.GetBySlugAsync(slug);
        return category;
    }

    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        return category;
    }
    
    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return categories.Select(category => new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Description = category.Description,
        });
    }
}