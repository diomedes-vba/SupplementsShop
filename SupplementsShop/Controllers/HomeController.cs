using Microsoft.AspNetCore.Mvc;

namespace SupplementsShop.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}