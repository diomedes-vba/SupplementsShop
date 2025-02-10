using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Models;

namespace SupplementsShop.Controllers;

public class CategoriesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Edit(int? id)
    {
        var category = new Category { Id = id.HasValue ? id.Value : 0 };
        
        return View(category);
    }
}