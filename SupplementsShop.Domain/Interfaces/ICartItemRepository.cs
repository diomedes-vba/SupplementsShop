using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Interfaces;

public interface ICartItemRepository
{
    Task AddToCartAsync(CartItemContext cartItem);
}