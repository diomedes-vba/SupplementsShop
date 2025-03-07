using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task CreateOrderAsync(OrderDto order, CartDto cart)
    {
        var orderNumber = await _orderRepository.GetNextOrderNumberAsync();
        await _orderRepository.AddAsync(new Order(
            orderNumber,
            order.FirstName,
            order.LastName,
            order.OrderDate,
            order.Email,
            order.PhoneNumber,
            order.StreetAddress1,
            order.StreetAddress2,
            order.City,
            order.StateOrRegion,
            order.PostalCode,
            order.Country,
            order.OrderItems));
    }
}