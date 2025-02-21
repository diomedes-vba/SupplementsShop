using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplementsShop.Data;

namespace SupplementsShop.Controllers;

public class ProductController : Controller
{
    private readonly SupplementsShopContext _context;
    public ProductController(SupplementsShopContext context)
    {
        _context = context;
    }

    // GET
    public async Task<IActionResult> Details(string slug)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.ProductSlug == slug);
        return View(product);
    }
}