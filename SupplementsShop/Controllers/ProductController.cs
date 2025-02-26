using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.Services;

namespace SupplementsShop.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // GET
    public async Task<IActionResult> Details(string slug)
    {
        var product = await _productService.GetProductBySlugAsync(slug);
        return View(product);
    }
}