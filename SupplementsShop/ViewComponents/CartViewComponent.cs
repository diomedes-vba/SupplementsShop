using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.Services;
using SupplementsShop.Application.DTOs;

namespace SupplementsShop.ViewComponents;

public class CartViewComponent : ViewComponent
{
    private readonly ICartService _cartService;

    public CartViewComponent(ICartService cartService)
    {
        _cartService = cartService;
    }

    public Task<IViewComponentResult> InvokeAsync()
    {
        CartDto currentCart = _cartService.GetCart();
        return Task.FromResult((IViewComponentResult)View(currentCart));
    }
}