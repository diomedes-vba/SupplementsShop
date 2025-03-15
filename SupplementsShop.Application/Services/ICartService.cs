using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Models;

namespace SupplementsShop.Application.Services;

public interface ICartService
{
    CartDto GetCart();
    Task<bool> AddToCartAsync(int productId, int quantity, string? userId);
    Task<bool> UpdateItemQuantityAsync(int productId, int quantity);
    decimal GetCartTotalPrice();
    decimal GetCartItemTotalPrice(int productId);
    void RemoveFromCart(int productId);
    void ClearCartSession();
    Task ClearCartContextAsync(string userId);
    int GetCartCount();
    Task MergeCartAsync(string? userId);
}