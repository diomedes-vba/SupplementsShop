using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SupplementsShop.Domain.Interfaces;
using SupplementsShop.Domain.Models;
using SupplementsShop.Infrastructure.Extensions;
using SupplementsShop.Application.DTOs;

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Application.Services;

public class CartService : ICartService
{
    private const string CartSessionKey = "Cart";
    private readonly IProductRepository _productRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<CartService> _logger;
    private readonly ICartItemRepository _cartItemRepository;

    public CartService(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor, ILogger<CartService> logger, ICartItemRepository cartItemRepository)
    {
        _productRepository = productRepository;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _cartItemRepository = cartItemRepository;
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

    public async Task<bool> AddToCartAsync(int productId, int quantity, string? userId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) return false;

        if (userId != null)
        {
            await AddToContextCartAsync(productId, quantity, userId);
        }
        
        AddToSessionCart(new CartItem
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Quantity = quantity,
        });
        return true;
    }

    private async Task AddToContextCartAsync(int productId, int quantity, string? userId)
    {
        var cartItemId = await _cartItemRepository.GetCartItemIdAsync(productId, userId);
        if (cartItemId == null)
        {
            await _cartItemRepository.AddToCartAsync(new CartItemContext
            {
                ProductId = productId,
                Quantity = quantity,
                UserId = userId
            });
        }
        else
        {
            await _cartItemRepository.IncreaseQuantityAsync(cartItemId, quantity);
        }
    }

    private void AddToSessionCart(CartItem cartItem)
    {
        var cart = GetCartFromSession();
        cart.AddItem(cartItem);
        SaveCartToSession(cart);
    }

    public async Task<bool> UpdateItemQuantityAsync(int productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) return false;
        
        var cart = GetCartFromSession();
        cart.UpdateItemQuantity(productId, quantity);
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

    public void ClearCartSession()
    {
        var cart = GetCartFromSession();
        cart.Clear();
        SaveCartToSession(cart);
    }

    public async Task ClearCartContextAsync(string userId)
    {
        await _cartItemRepository.ClearUserItemsAsync(userId);
    }

    public int GetCartCount()
    {
        var cart = GetCartFromSession();
        return cart.Items.Count;
    }

    public async Task MergeCartAsync(string? userId)
    {
        var cartItemsSessionPreMerge = GetCartFromSession().Items;
        
        var cartItemsFromContext = await _cartItemRepository.GetCartItemsAsync(userId);
        var cartItemsToSession = CartItemsFromContextToSession(cartItemsFromContext);

        foreach (var cartItem in cartItemsToSession)
        {
            AddToSessionCart(cartItem);
        }

        foreach (var cartItem in cartItemsSessionPreMerge)
        {
            await AddToContextCartAsync(cartItem.Id, cartItem.Quantity, userId);
        }
    }

    private List<CartItem> CartItemsFromContextToSession(List<CartItemContext> cartItemsContext)
    {
        var cartItems = cartItemsContext.Select(ci => new CartItem
        {
            Id = ci.ProductId,
            Name = ci.Product.Name,
            Price = ci.Product.Price,
            Quantity = ci.Quantity,
            ImageUrl = ci.Product.ImageUrl
        }).ToList();
        
        return cartItems;
    }
}