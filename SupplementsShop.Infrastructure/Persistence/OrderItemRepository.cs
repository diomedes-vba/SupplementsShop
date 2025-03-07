using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Infrastructure.Persistence;

public class OrderItemRepository : IOrderItemRepository
{
    private SupplementsShopContext _context;

    public OrderItemRepository(SupplementsShopContext context)
    {
        _context = context;
    }
    
    public async Task AddOrderItemAsync(OrderItem orderItem)
    {
        await _context.OrderItems.AddAsync(orderItem);
        await _context.SaveChangesAsync();
    }
}