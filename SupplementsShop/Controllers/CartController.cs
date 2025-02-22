using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Models;

namespace SupplementsShop.Controllers;

public class CartController : Controller
{
    // GET
    public IActionResult Summary()
    {
        
        return View();
    }
}