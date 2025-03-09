using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Interfaces;

public interface IOrderItemRepository
{
    Task AddOrderItemRangeAsync(List<OrderItem> orderItems);
}