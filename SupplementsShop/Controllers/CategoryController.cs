using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.Services;

namespace SupplementsShop.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        
        _categoryService = categoryService;
    }
    
    // GET
    public async Task<IActionResult> Products(string slug, int page = 1)
    {
        var category = await _categoryService.GetCategoryBySlugAsync(slug, page);
        if (category == null) return NotFound();
        return View(category);
    }
}