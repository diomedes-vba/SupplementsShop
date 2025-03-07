using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Interfaces;

public interface IOrderItemRepository
{
    Task AddOrderItemAsync(OrderItem orderItem);
}