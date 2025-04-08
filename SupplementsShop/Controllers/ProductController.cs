using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.DTOs;
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

    public async Task<IActionResult> Search(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            return View(new List<ProductDto>());
        }
        
        return View(new List<ProductDto>());
    }

    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        return View();
    }

    /*
    [HttpPost]
    public async Task<IActionResult> Edit(ProductViewModel productViewModel)
    {
        return RedirectToAction("Details", "Product", productViewModel.String);
    }
    */
}