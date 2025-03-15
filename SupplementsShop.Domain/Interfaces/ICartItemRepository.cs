using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Interfaces;

public interface ICartItemRepository
{
    Task AddToCartAsync(CartItemContext cartItem);
    Task<List<CartItemContext>> GetCartItemsAsync(string? userId);
    Task<int?> GetCartItemIdAsync(int productId, string userId);
    Task IncreaseQuantityAsync(int? cartItemId, int quantity);
    Task ClearUserItemsAsync(string? userId);
}