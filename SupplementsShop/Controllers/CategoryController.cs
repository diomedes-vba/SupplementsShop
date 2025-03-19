using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Application.Services;
using SupplementsShop.ViewModels;

namespace SupplementsShop.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        
        _categoryService = categoryService;
    }
    
    // GET
    public async Task<IActionResult> Products(string slug)
    {
        var category = await _categoryService.GetCategoryBySlugAsync(slug, 1);
        if (category == null) return NotFound();
        return View(category);
    }

    public async Task<IActionResult> ShowProducts(string slug)
    {
        var productList = new ProductsListModel
        {
            Products = new List<ProductDto>(),
            CurrentPage = 1,
            TotalPages = 10,
            TotalProducts = 390,
            PageSize = 40,
        };
        return View(productList);
    }
    
}