using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto?> GetCategoryBySlugAsync(string slug, int pageNumber)
    {
        var category = await _categoryRepository.GetBySlugAsync(slug, pageNumber);
        if (category == null) return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Description = category.Description,
            Products = category.CategoryProducts.Select(cp => new ProductDto
            {
                Id = cp.Product.Id,
                Name = cp.Product.Name,
                Price = cp.Product.Price,
                ImageUrl = cp.Product.ImageUrl,
                Slug = cp.Product.Slug,
                Description = cp.Product.Description,
                Sales = cp.Product.Sales
            }).ToList()
        };
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