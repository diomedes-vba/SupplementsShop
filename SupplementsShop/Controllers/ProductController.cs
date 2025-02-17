using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Models;

namespace SupplementsShop.Controllers;

public class ProductController : Controller
{
    // GET
    public IActionResult Details(string slug)
    {
        var category = CategoriesRepository.GetCategories().FirstOrDefault(c => c.CategorySlug == slug);
        return View(category);
    }
}