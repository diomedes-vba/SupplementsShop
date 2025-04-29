using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.Services;
using SupplementsShop.Web.Factories;

namespace SupplementsShop.Web.Controllers;

public class HomeController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly ICategoryModelFactory _categoryModelFactory;

    public HomeController(ICategoryService categoryService, ICategoryModelFactory categoryModelFactory)
    {
        _categoryService = categoryService;
        _categoryModelFactory = categoryModelFactory;
    }
    
    public async  Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        var categoryList = _categoryModelFactory.PrepareHomeCategoryViewModels(categories);
        return View(categoryList);
    }
}