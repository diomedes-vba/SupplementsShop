using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Application.Services;
using SupplementsShop.Factories;

namespace SupplementsShop.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IProductModelFactory _productModelFactory;
    public ProductController(IProductService productService, IProductModelFactory productModelFactory)
    {
        _productService = productService;
        _productModelFactory = productModelFactory;
    }

    // GET
    public async Task<IActionResult> Details(string slug)
    {
        var product = await _productService.GetProductBySlugAsync(slug);
        var productDto = _productModelFactory.PrepareProductDto(product);
        return View(productDto);
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
        var product = _productModelFactory.PrepareProductDto(await _productService.GetProductByIdAsync(id));
        return View(product);
    }

    
    [HttpPost]
    public async Task<IActionResult> Edit(ProductDto product)
    {
        await _productService.UpdateProduct(_productModelFactory.PrepareProductFromProductDto(product));
        var updatedProduct = await _productService.GetProductByIdAsync(product.Id);
        return RedirectToAction("Details", "Product", new { slug = updatedProduct?.Slug });
    }
    
}