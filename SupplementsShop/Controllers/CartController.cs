using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.Services;

namespace SupplementsShop.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }
    
    // GET
    public IActionResult Index()
    {
        var cart = _cartService.GetCart();
        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
    {
        if (await _cartService.AddToCartAsync(productId, quantity))
        {
            return Json(new { success = true, message = $"Added {quantity} product to cart" });
        }

        return Json(new { success = false, message = "Product not found" });
    }

    public IActionResult RemoveFromCart(int productId)
    {
        _cartService.RemoveFromCart(productId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult GetCartCount()
    {
        var cartCount = _cartService.GetCartCount();
        return Json(new { cartCount });
    }
}