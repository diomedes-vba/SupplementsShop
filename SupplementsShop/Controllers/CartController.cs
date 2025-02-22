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

    public async Task<IActionResult> AddToCart(int productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        if (product != null)
        {
            var cart = GetCart();
            cart.AddItem(product);
            HttpContext.Session.SetObject(CartSessionKey, cart);
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> RemoveFromCart(int productId)
    {
        var cart = GetCart();
        cart.RemoveItem(productId);
        HttpContext.Session.SetObject(CartSessionKey, cart);
        return RedirectToAction("Index");
    }
}