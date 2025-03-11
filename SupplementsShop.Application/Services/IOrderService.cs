using SupplementsShop.Application.DTOs;

namespace SupplementsShop.Application.Services;

public interface IOrderService
{
    Task<int?> CreateOrderAsync(OrderDto order, CartDto cart);
    Task<OrderDto> GetOrderByIdAsync(int orderId);
}