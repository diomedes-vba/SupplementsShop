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
    
    public async Task AddOrderItemRangeAsync(List<OrderItem> orderItems)
    {
        await _context.OrderItems.AddRangeAsync(orderItems);
        await _context.SaveChangesAsync();
    }
    
    
}