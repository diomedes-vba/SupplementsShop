using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Interfaces;

public interface ICartItemRepository
{
    Task AddToCartAsync(CartItemContext cartItem);
    Task<List<CartItemContext>> GetCartItemsAsync(string? userId);
}