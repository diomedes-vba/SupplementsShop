using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Application.Services;
using SupplementsShop.Factories;
using SupplementsShop.ViewModels;

namespace SupplementsShop.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IProductModelFactory _productModelFactory;
    private readonly IImageService _imageService;
    public ProductController(IProductService productService, IProductModelFactory productModelFactory, IImageService imageService)
    {
        _productService = productService;
        _productModelFactory = productModelFactory;
        _imageService = imageService;
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
        var product = await _productService.GetProductByIdAsync(id);
        var productEditViewModel = _productModelFactory.PrepareProductEditViewModel(product);
        return View(productEditViewModel);
    }

    
    [HttpPost]
    public async Task<IActionResult> Edit(ProductEditViewModel productViewModel)
    {
        if (!ModelState.IsValid)
            return View(productViewModel);
        
        productViewModel.ImageUrl = await _imageService.SaveImageAsync(productViewModel.ImageFile);
        var product = await _productModelFactory.PrepareProductFromProductEditViewModelAsync(productViewModel);
        await _productService.UpdateProduct(product);
        
        var updatedProduct = await _productService.GetProductByIdAsync(product.Id);
        return RedirectToAction("Details", "Product", new { slug = updatedProduct?.Slug });
    }
    
}