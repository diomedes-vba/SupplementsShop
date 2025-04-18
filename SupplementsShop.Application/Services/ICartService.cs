using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Models;

namespace SupplementsShop.Application.Services;

public interface ICartService
{
    Cart GetCart();
    Task<bool> AddToCartAsync(int productId, int quantity, string? userId);
    Task<bool> UpdateItemQuantityAsync(int productId, int quantity, string? userId);
    decimal GetCartTotalPrice();
    decimal GetCartItemTotalPrice(int productId);
    Task RemoveFromCart(int productId, string? userId);
    void ClearCartSession();
    Task ClearCartContextAsync(string userId);
    int GetCartCount();
    Task MergeCartAsync(string? userId);
}