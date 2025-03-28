using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Interfaces;

public interface ICartItemRepository
{
    Task AddToCartAsync(CartItemContext cartItem);
    Task RemoveFromCartAsync(CartItemContext cartItem);
    Task UpdateCartItemAsync(CartItemContext cartItem);
    Task<CartItemContext?> GetCartItemAsync(string? userId, int productId);
    Task<List<CartItemContext>> GetCartItemListAsync(string? userId);
    Task<int?> GetCartItemIdAsync(int productId, string userId);
    Task IncreaseQuantityAsync(int? cartItemId, int quantity);
    Task ClearUserItemsAsync(string? userId);
}