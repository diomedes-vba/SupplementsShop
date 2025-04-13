using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int orderId);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(int orderId);
    int GetNextOrderNumberAsync();
}