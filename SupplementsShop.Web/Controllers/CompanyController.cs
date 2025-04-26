using Microsoft.AspNetCore.Mvc;

namespace SupplementsShop.Web.Controllers;

public class CompanyController : Controller
{
    // GET
    public IActionResult Details(string slug)
    {
        return View();
    }
}