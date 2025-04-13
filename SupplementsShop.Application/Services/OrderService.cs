using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;
using SupplementsShop.Domain.Models;

namespace SupplementsShop.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
    }

    public async Task<int> CreateOrderAsync(Order order, IList<OrderItem> orderItems)
    {
        var orderNumber = _orderRepository.GetNextOrderNumberAsync();
        order.SetOrderNumber(orderNumber);
        order.SetOrderDate(DateTime.Now);
        
        await _orderRepository.AddAsync(order);

        await AddOrderItemsToOrderAsync(order, orderItems);

        return order.OrderNumber;
    }

    private async Task AddOrderItemsToOrderAsync(Order order, IList<OrderItem> orderItems)
    {
        order.AddItems(orderItems);
        await _orderRepository.UpdateAsync(order);
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        return order;
    }
}