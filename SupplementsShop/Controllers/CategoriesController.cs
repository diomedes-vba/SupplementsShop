using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Models;

namespace SupplementsShop.Controllers;

public class CategoriesController : Controller
{
    // GET
    public IActionResult Index()
    {
        var categories = CategoriesRepository.GetCategories();
        return View(categories);
    }

    public IActionResult Edit(int? id)
    {
        var category = CategoriesRepository.GetCategoryById(id.HasValue ? id.Value : 0);
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            CategoriesRepository.UpdateCategory(category.CategoryId, category);
            return RedirectToAction("Index");
        }

        return View(category);
    }

    public IActionResult Delete(int? id)
    {
        var category = CategoriesRepository.GetCategoryById(id.HasValue ? id.Value : 0);
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirm(int? id)
    {
        CategoriesRepository.DeleteCategory(id.HasValue ? id.Value : 0);
        return RedirectToAction("Index");
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            CategoriesRepository.AddCategory(category);
            return RedirectToAction("Index");
        }

        return View(category);
    }

    public IActionResult List()
    {
        var categories = CategoriesRepository.GetCategories();
        return View(categories);
    }
}