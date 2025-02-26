using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Models;

namespace SupplementsShop.Application.Services;

public interface ICartService
{
    CartDto GetCart();
    Task<bool> AddToCartAsync(int productId, int quantity);
    void RemoveFromCart(int productId);
    void ClearCart();
    int GetCartCount();
}