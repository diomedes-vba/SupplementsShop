using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Application.Services;
using SupplementsShop.ViewModels;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;
    private readonly IPaymentService _paymentService;
    private readonly UserManager<User> _userManager;

    public CartController(ICartService cartService, IOrderService orderService, IPaymentService paymentService, UserManager<User> userManager)
    {
        _cartService = cartService;
        _orderService = orderService;
        _paymentService = paymentService;
        _userManager = userManager;
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
        var userId = _userManager.GetUserId(User);
        if (await _cartService.AddToCartAsync(productId, quantity, userId))
        {
            return Json(new {
                success = true, 
                message = $"Added {quantity} product to cart"
            });
        }

        return Json(new { success = false, message = "Product not found" });
    }

    [HttpGet]
    public IActionResult RefreshCart()
    {
        return ViewComponent("Cart");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateItemQuantity(int productId, int quantity)
    {
        if (await _cartService.UpdateItemQuantityAsync(productId, quantity))
        {
            var cartTotalPrice = _cartService.GetCartTotalPrice();
            var cartItemTotalPrice = _cartService.GetCartItemTotalPrice(productId);
            return Json(new { 
                success = true, 
                cartNewPrice = cartTotalPrice, 
                itemNewPrice = cartItemTotalPrice, 
                message = $"Product quantity updated to {quantity}" 
            });
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
        var userId = _userManager.GetUserId(User);
        var checkoutModel = new CheckoutViewModel
        {
            Cart = _cartService.GetCart(),
            Order = new OrderDto { UserId = userId}
        };
        return View(checkoutModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutViewModel checkoutModel)
    {
        if (!ModelState.IsValid)
        {
            checkoutModel.Cart = _cartService.GetCart();
            return View(checkoutModel);
        }
        
        var cart = _cartService.GetCart();
        var order = checkoutModel.Order;
        var orderNumber = await _orderService.CreateOrderAsync(order, cart);
        
        return RedirectToAction("Payment", new { orderNumber, order.UserId });
    }

    [Authorize]
    [HttpGet]
    public IActionResult Payment(int? orderNumber, string userId)
    {
        var paymentModel = new PaymentViewModel
        {
            Cart = _cartService.GetCart(),
            OrderNumber = orderNumber,
            UserId = userId
        };
        return View(paymentModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Payment(PaymentViewModel paymentModel)
    {
        if (!ModelState.IsValid) return View(paymentModel);

        var paymentInformation = new PaymentDto
        {
            CardNumber = paymentModel.CardNumber,
            ExpirationDate = paymentModel.ExpirationDate,
            CVV = paymentModel.CVV
        };
        var result = await _paymentService.ProcessPaymentAsync(paymentInformation);

        if (result.Success)
        {
            _cartService.ClearCartSession();
            await _cartService.ClearCartContextAsync(paymentModel.UserId);
            return RedirectToAction("ThanksForOrder", new { orderNumber = paymentModel.OrderNumber });
        }
        
        ModelState.AddModelError(string.Empty, result.ErrorMessage);
        return View(paymentModel);
    }

    public IActionResult ThanksForOrder(int orderNumber)
    {
        return View(orderNumber);
    }
}