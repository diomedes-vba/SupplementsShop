using Microsoft.EntityFrameworkCore;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Infrastructure.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly SupplementsShopContext _context;

    public OrderRepository(SupplementsShopContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(int orderId)
    {
        return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }

    public int GetNextOrderNumberAsync()
    {
        int? lastOrderNumber = _context.Orders.Max(o => o.OrderNumber);
        return lastOrderNumber + 1 ?? 1000;
    }
}