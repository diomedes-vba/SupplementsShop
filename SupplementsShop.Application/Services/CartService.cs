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

    private void SaveCartToSession(Cart cart)
    {
        Session.SetObject(CartSessionKey, cart);
    }

    public Cart GetCart()
    {
        var cart = Session.GetObject<Cart>(CartSessionKey) ?? new Cart();
        return cart;
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
            ProductNumber = product.ProductNumber
        });
        return true;
    }

    private async Task AddToContextCartAsync(int productId, int quantity, string? userId)
    {
        var cartItemId = await _cartItemRepository.GetCartItemIdAsync(productId, userId);
        if (cartItemId == null)
        {
            await _cartItemRepository.AddToCartAsync(new CartItemContext
            (
                productId: productId,
                quantity: quantity,
                userId : userId
            ));
        }
        else
        {
            await _cartItemRepository.IncreaseQuantityAsync(cartItemId, quantity);
        }
    }

    private void AddToSessionCart(CartItem cartItem)
    {
        var cart = GetCart();
        cart.AddItem(cartItem);
        SaveCartToSession(cart);
    }

    public async Task<bool> UpdateItemQuantityAsync(int productId, int quantity, string? userId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) return false;
        
        var cart = GetCart();
        cart.UpdateItemQuantity(productId, quantity);
        SaveCartToSession(cart);

        if (userId != null)
        {
            var cartItem = await _cartItemRepository.GetCartItemAsync(userId, productId);
            if (cartItem != null)
            {
                cartItem.UpdateCartItemQuantity(quantity);
                await _cartItemRepository.UpdateCartItemAsync(cartItem);
            }
        }
        
        return true;
    }

    public decimal GetCartTotalPrice()
    {
        var cart = GetCart();
        return cart.TotalPrice;
    }

    public decimal GetCartItemTotalPrice(int productId)
    {
        var cart = GetCart();
        return cart.Items.FirstOrDefault(item => item.Id == productId)?.TotalPrice ?? 0;
    }

    public async Task RemoveFromCart(int productId, string? userId)
    {
        var cart = GetCart();
        cart.RemoveItem(productId);
        SaveCartToSession(cart);

        if (userId != null)
        {
            var cartItem = await _cartItemRepository.GetCartItemAsync(userId, productId);
            if (cartItem != null)
            {
                await _cartItemRepository.RemoveFromCartAsync(cartItem);
            }
        }
    }

    public void ClearCartSession()
    {
        var cart = GetCart();
        cart.Clear();
        SaveCartToSession(cart);
    }

    public async Task ClearCartContextAsync(string userId)
    {
        await _cartItemRepository.ClearUserItemsAsync(userId);
    }

    public int GetCartCount()
    {
        var cart = GetCart();
        return cart.Items.Count;
    }

    public async Task MergeCartAsync(string? userId)
    {
        if (userId == null)
            return;

        if (Session.GetString("CartMerged") != "true")
        {
            var cartItemsSessionPreMerge = GetCart().Items;
        
            var cartItemsFromContext = await _cartItemRepository.GetCartItemListAsync(userId);
            var cartItemsToSession = CartItemsFromContextToSession(cartItemsFromContext);

            foreach (var cartItem in cartItemsToSession)
            {
                AddToSessionCart(cartItem);
            }

            foreach (var cartItem in cartItemsSessionPreMerge)
            {
                await AddToContextCartAsync(cartItem.Id, cartItem.Quantity, userId);
            }
            
            Session.SetString("CartMerged", "true");
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
            ImageUrl = ci.Product.ImageUrl,
            ProductNumber = ci.Product.ProductNumber
        }).ToList();
        
        return cartItems;
    }
}