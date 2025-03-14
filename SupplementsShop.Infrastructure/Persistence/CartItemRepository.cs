using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Infrastructure.Persistence;

public class CartItemRepository : ICartItemRepository
{
    private readonly SupplementsShopContext _context;

    public CartItemRepository(SupplementsShopContext context)
    {
        _context = context;
    }
    
    public async Task AddToCartAsync(CartItemContext cartItem)
    {
        await _context.CartItems.AddAsync(cartItem);
        await _context.SaveChangesAsync();
    }
}