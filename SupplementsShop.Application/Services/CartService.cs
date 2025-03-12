using Microsoft.AspNetCore.Http;
using SupplementsShop.Domain.Interfaces;
using SupplementsShop.Domain.Models;
using SupplementsShop.Infrastructure.Extensions;
using SupplementsShop.Application.DTOs;

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SupplementsShop.Application.Services;

public class CartService : ICartService
{
    private const string CartSessionKey = "Cart";
    private readonly IProductRepository _productRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<CartService> _logger;

    public CartService(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor, ILogger<CartService> logger)
    {
        _productRepository = productRepository;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    
    private ISession Session => _httpContextAccessor.HttpContext.Session;

    private Cart GetCartFromSession()
    {
        var cart = Session.GetObject<Cart>(CartSessionKey) ?? new Cart();
        return cart;
    }

    private void SaveCartToSession(Cart cart)
    {
        Session.SetObject(CartSessionKey, cart);
    }

    public CartDto GetCart()
    {
        var cart = GetCartFromSession();

        return new CartDto
        {
            Items = cart.Items.Select(item => new CartItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                ImageUrl = item.ImageUrl,
                Quantity = item.Quantity
            }).ToList(),
            TotalPrice = cart.TotalPrice
        };
    }

    public async Task<bool> AddToCartAsync(int productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) return false;
        
        var cart = GetCartFromSession();
        cart.AddItem(product, quantity);
        SaveCartToSession(cart);
        return true;
    }

    public async Task<bool> UpdateItemQuantityAsync(int productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) return false;
        
        var cart = GetCartFromSession();
        cart.UpdateItemQuantity(product, quantity);
        SaveCartToSession(cart);
        return true;
    }

    public decimal GetCartTotalPrice()
    {
        var cart = GetCartFromSession();
        return cart.TotalPrice;
    }

    public decimal GetCartItemTotalPrice(int productId)
    {
        var cart = GetCartFromSession();
        return cart.Items.FirstOrDefault(item => item.Id == productId)?.TotalPrice ?? 0;
    }

    public void RemoveFromCart(int productId)
    {
        var cart = GetCartFromSession();
        cart.RemoveItem(productId);
        SaveCartToSession(cart);
    }

    public void ClearCart()
    {
        var cart = GetCartFromSession();
        cart.Clear();
        SaveCartToSession(cart);
    }

    public int GetCartCount()
    {
        var cart = GetCartFromSession();
        return cart.Items.Count;
    }

}