using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplementsShop.Data;

namespace SupplementsShop.Controllers;

public class CategoriesController : Controller
{
    private readonly SupplementsShopContext _context;
    
    public CategoriesController(SupplementsShopContext context)
    {
        _context = context;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var categories = await _context.Categories.ToListAsync();
        return View(categories);
    }
    
}