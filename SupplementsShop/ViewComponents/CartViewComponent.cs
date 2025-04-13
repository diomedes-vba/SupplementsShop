using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.Services;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Factories;

namespace SupplementsShop.ViewComponents;

public class CartViewComponent : ViewComponent
{
    private readonly ICartService _cartService;
    private readonly ICartModelFactory _cartModelFactory;

    public CartViewComponent(ICartService cartService, ICartModelFactory cartModelFactory)
    {
        _cartService = cartService;
        _cartModelFactory = cartModelFactory;
    }

    public Task<IViewComponentResult> InvokeAsync()
    {
        var currentCart = _cartService.GetCart();
        var cartModel = _cartModelFactory.PrepareCartViewModel(currentCart);
        return Task.FromResult((IViewComponentResult)View(cartModel));
    }
}