using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Models;

namespace SupplementsShop.Application.Services;

public interface ICartService
{
    CartDto GetCart();
    Task<bool> AddToCartAsync(int productId, int quantity);
    Task<bool> UpdateItemQuantityAsync(int productId, int quantity);
    decimal GetCartTotalPrice();
    decimal GetCartItemTotalPrice(int productId);
    void RemoveFromCart(int productId);
    void ClearCart();
    int GetCartCount();
}