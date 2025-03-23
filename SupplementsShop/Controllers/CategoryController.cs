using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Application.Services;
using SupplementsShop.Factories;
using SupplementsShop.ViewModels;

namespace SupplementsShop.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly ICategoryModelFactory _categoryModelFactory;
    private readonly IProductModelFactory _productModelFactory;
    public CategoryController(ICategoryService categoryService,IProductService productService, ICategoryModelFactory categoryModelFactory, IProductModelFactory productModelFactory)
    {
        _categoryService = categoryService;
        _categoryModelFactory = categoryModelFactory;
        _productService = productService;
        _productModelFactory = productModelFactory;
    }
    
    // GET
    public async Task<IActionResult> Products(string slug)
    {
        var category = await _categoryService.GetCategoryBySlugAsync(slug, 1);
        if (category == null) return NotFound();
        return View(category);
    }

    public async Task<IActionResult> CategoryPage(string slug)
    {
        var category = await _categoryService.GetCategoryBySlugAsync(slug);
        if (category == null) return NotFound();
        
        var productsList = await _productService.GetProductListByCategoryIdAsync(
            category.Id, 
            page: 0,
            pageSize: 10);
        
        var categoryProductsList = _categoryModelFactory.PrepareCategoryProductsListModel(category, productsList);
            
        return View(categoryProductsList);
    }

    [HttpGet]
    public async Task<IActionResult> GetNewPage(int categoryId, int page, int pageSize)
    {
        var productsPagedList = await _productService.GetProductListByCategoryIdAsync(categoryId, page, pageSize);
        var productsList = _productModelFactory.PrepareProductDtos(productsPagedList);
        return PartialView("_ProductList", productsList);
    }
}