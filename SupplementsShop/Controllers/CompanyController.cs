using Microsoft.AspNetCore.Mvc;

namespace SupplementsShop.Controllers;

public class CompanyController : Controller
{
    // GET
    public IActionResult Details(string slug)
    {
        return View();
    }
}