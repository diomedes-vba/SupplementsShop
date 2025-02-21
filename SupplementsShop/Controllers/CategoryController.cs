using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Data;
using Microsoft.EntityFrameworkCore;

namespace SupplementsShop.Controllers;

public class CategoryController : Controller
{
    private readonly SupplementsShopContext _context;
    public CategoryController(SupplementsShopContext context)
    {
        _context = context;
    }
    
    // GET
    public async Task<IActionResult> Products(string slug)
    {
        var category = await _context.Categories
            .Include(c => c.CategoryProducts)
            .FirstOrDefaultAsync(c => c.CategoryThug == slug);
        return View(category);
    }
}