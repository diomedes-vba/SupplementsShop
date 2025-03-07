using SupplementsShop.Application.DTOs;

namespace SupplementsShop.Application.Services;

public interface IOrderService
{
    Task CreateOrderAsync(OrderDto order, CartDto cart);
}