using SupplementsShop.Domain.Models;

namespace SupplementsShop.Application.Services;

public interface ICartService
{
    void AddToCart(Cart cart, int productId, int quantity);
    void RemoveFromCart(Cart cart, int productId);
    decimal GetTotalPrice(Cart cart);
    void ClearCart(Cart cart);
}