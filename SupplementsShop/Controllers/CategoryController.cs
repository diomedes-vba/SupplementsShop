using Microsoft.AspNetCore.Mvc;

namespace SupplementsShop.Controllers;

public class CategoryController : Controller
{
    // GET
    public IActionResult Products(string slug)
    {
        return View();
    }
}