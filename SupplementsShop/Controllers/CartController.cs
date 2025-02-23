using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplementsShop.Data;
using SupplementsShop.Models;

namespace SupplementsShop.Controllers;

public class CartController : Controller
{
    private readonly SupplementsShopContext _context;
    private const string CartSessionKey = "Cart";

    public CartController(SupplementsShopContext context)
    {
        _context = context;
    }
    private Cart GetCart()
    {
        var cart = HttpContext.Session.GetObject<Cart>(CartSessionKey);
        if (cart == null)
        {
            cart = new Cart();
            HttpContext.Session.SetObject(CartSessionKey, cart);
        }

        return cart;
    }
    
    // GET
    public IActionResult Index()
    {
        var cart = GetCart();
        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        if (product != null)
        {
            var cart = GetCart();
            cart.AddItem(product, quantity);
            HttpContext.Session.SetObject(CartSessionKey, cart);
            return Json(new { success = true, message = $"Added {quantity} product to cart" });
        }

        return Json(new { success = false, message = "Product not found" });
    }

    public async Task<IActionResult> RemoveFromCart(int productId)
    {
        var cart = GetCart();
        cart.RemoveItem(productId);
        HttpContext.Session.SetObject(CartSessionKey, cart);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult GetCartCount()
    {
        var cart = GetCart();
        int count = cart.Items.Count;
        return Json(new { count });
    }
}