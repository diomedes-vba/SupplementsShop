using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Application.Services;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using SupplementsShop.Factories;
using SupplementsShop.ViewModels;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;
    private readonly IPaymentService _paymentService;
    private readonly UserManager<User> _userManager;
    private readonly ICartModelFactory _cartModelFactory;
    private readonly IOrderModelFactory _orderModelFactory;

    public CartController(ICartService cartService, 
        IOrderService orderService, 
        IPaymentService paymentService, 
        UserManager<User> userManager,
        ICartModelFactory cartModelFactory,
        IOrderModelFactory orderModelFactory)
    {
        _cartService = cartService;
        _orderService = orderService;
        _paymentService = paymentService;
        _userManager = userManager;
        _cartModelFactory = cartModelFactory;
        _orderModelFactory = orderModelFactory;
    }
    
    // GET
    public IActionResult Index()
    {
        var cart = _cartService.GetCart();
        var cartModel = _cartModelFactory.PrepareCartViewModel(cart);
        return View(cartModel);
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
        var userId = _userManager.GetUserId(User);
        if (await _cartService.UpdateItemQuantityAsync(productId, quantity, userId))
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

    public async Task<IActionResult> RemoveFromCart(int productId)
    {
        var userId = _userManager.GetUserId(User);
        await _cartService.RemoveFromCart(productId, userId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> GetCartCount()
    {
        var userId = _userManager.GetUserId(User);
        await _cartService.MergeCartAsync(userId);
        
        var cartCount = _cartService.GetCartCount();
        return Json(new { cartCount });
    }

    [Authorize]
    [HttpGet]
    public IActionResult Checkout()
    {
        var userId = _userManager.GetUserId(User);
        var cart = _cartService.GetCart();
        var cartModel = _cartModelFactory.PrepareCartViewModel(cart);
        var checkoutModel = new CheckoutViewModel
        {
            Cart = cartModel,
            Order = new OrderViewModel { UserId = userId }
        };
        return View(checkoutModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutViewModel checkoutModel)
    {
        var cart = _cartService.GetCart();
        var cartModel = _cartModelFactory.PrepareCartViewModel(cart);
        if (!ModelState.IsValid)
        {
            checkoutModel.Cart = cartModel;
            return View(checkoutModel);
        }
        
        var orderModel = checkoutModel.Order;
        var order = _orderModelFactory.PrepareOrderFromViewModel(orderModel);
        var orderItems = _orderModelFactory.PrepareOrderItemsFromCart(cart.Items);
        
        var orderNumber = await _orderService.CreateOrderAsync(order, orderItems);

        if (orderNumber == null)
            return RedirectToAction("OrderFailed");
        
        return RedirectToAction("Payment", new { orderNumber, order.UserId });
    }

    [Authorize]
    [HttpGet]
    public IActionResult Payment(int? orderNumber, string userId)
    {
        var cart = _cartService.GetCart();
        var cartModel = _cartModelFactory.PrepareCartViewModel(cart);
        var paymentModel = new PaymentViewModel
        {
            Cart = cartModel,
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

    public IActionResult OrderFailed()
    {
        return View();
    }
}