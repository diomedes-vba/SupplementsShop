using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Application.Services;
using SupplementsShop.ViewModels;

namespace SupplementsShop.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;

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

    [Authorize]
    [HttpGet]
    public IActionResult Checkout()
    {
        var checkoutModel = new CheckoutViewModel
        {
            Cart = _cartService.GetCart(),
            Order = new OrderDto()
        };
        return View(checkoutModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutViewModel checkoutModel)
    {
        if (!ModelState.IsValid) return View(checkoutModel);
        
        var cart = _cartService.GetCart();
        var order = checkoutModel.Order;
        await _orderService.CreateOrderAsync(order, cart);
        
        return RedirectToAction("Payment");
    }

    [Authorize]
    [HttpGet]
    public IActionResult Payment()
    {
        var paymentModel = new PaymentViewModel
        {
            Cart = _cartService.GetCart(),
        };
        return View(paymentModel);
    }
}