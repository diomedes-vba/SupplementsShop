using Microsoft.EntityFrameworkCore;
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

    public async Task RemoveFromCartAsync(CartItemContext cartItem)
    {
        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCartItemAsync(CartItemContext cartItem)
    {
        _context.CartItems.Update(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task<CartItemContext?> GetCartItemAsync(string? userId, int productId)
    {
        var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);
        return cartItem;
    }
    
    public async Task<List<CartItemContext>> GetCartItemListAsync(string? userId)
    {
        var cartItems = await _context.CartItems
            .Where(ci => ci.UserId == userId)
            .Include(ci => ci.Product)
            .ToListAsync();
        
        return cartItems;
    }

    public async Task<int?> GetCartItemIdAsync(int productId, string? userId)
    {
        var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.UserId == userId);
        return cartItem?.Id;
    }

    public async Task IncreaseQuantityAsync(int? cartItemId, int quantity)
    {
        var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.Id == cartItemId);
        cartItem.IncreaseCartItemQuantity(quantity);
        await _context.SaveChangesAsync();
    }

    public async Task ClearUserItemsAsync(string? userId)
    {
        var itemsToRemove = await _context.CartItems.Where(ci => ci.UserId == userId).ToListAsync();
        _context.CartItems.RemoveRange(itemsToRemove);
        await _context.SaveChangesAsync();
    }
}