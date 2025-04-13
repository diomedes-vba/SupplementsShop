using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Models;

namespace SupplementsShop.Application.Services;

public interface IOrderService
{
    Task<int> CreateOrderAsync(Order order, IList<OrderItem> orderItems);
    Task<Order?> GetOrderByIdAsync(int orderId);
}