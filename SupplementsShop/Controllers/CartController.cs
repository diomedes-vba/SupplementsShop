using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Application.Services;
using SupplementsShop.ViewModels;
using Hangfire;

namespace SupplementsShop.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;
    private readonly IPaymentService _paymentService;

    public CartController(ICartService cartService, IOrderService orderService, IPaymentService paymentService)
    {
        _cartService = cartService;
        _orderService = orderService;
        _paymentService = paymentService;
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
        
        return RedirectToAction("Payment", new { orderNumber = order.OrderNumber });
    }

    [Authorize]
    [HttpGet]
    public IActionResult Payment(string orderNumber)
    {
        var paymentModel = new PaymentViewModel
        {
            Cart = _cartService.GetCart(),
            OrderNumber = orderNumber
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
            return RedirectToAction("ThanksForOrder", new { orderNumber = paymentModel.OrderNumber });
        }
        else
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return View(paymentModel);
        }
    }

    public IActionResult ThanksForOrder(string orderNumber)
    {
        return View(orderNumber);
    }
}