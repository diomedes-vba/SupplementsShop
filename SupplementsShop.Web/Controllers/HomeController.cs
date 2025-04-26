using Microsoft.AspNetCore.Mvc;

namespace SupplementsShop.Web.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}