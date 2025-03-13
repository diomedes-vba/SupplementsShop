using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

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

    public async Task<int?> CreateOrderAsync(OrderDto orderDto, CartDto cart)
    {
        var orderNumber = await _orderRepository.GetNextOrderNumberAsync();
        orderDto.OrderDate = DateTime.UtcNow;
        var order = new Order(
            orderNumber,
            orderDto.FirstName,
            orderDto.LastName,
            orderDto.OrderDate,
            orderDto.Email,
            orderDto.PhoneNumber,
            orderDto.StreetAddress1,
            orderDto.StreetAddress2,
            orderDto.City,
            orderDto.StateOrRegion,
            orderDto.PostalCode,
            orderDto.Country,
            orderDto.UserId);
        
        await _orderRepository.AddAsync(order);

        await AddOrderItemsToOrderAsync(order, cart);

        return order.OrderNumber;
    }

    private async Task AddOrderItemsToOrderAsync(Order order, CartDto cart)
    {
        var cartItems = cart.Items.ToList();
        var orderItems = cartItems.Select(i => new OrderItem
            (
                i.Name, 
                i.Price, 
                i.Quantity, 
                i.ImageUrl, 
                i.Id, 
                order.Id
            )).ToList();

        order.AddItems(orderItems);
        await _orderRepository.UpdateAsync(order);
    }

    public async Task<OrderDto> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        return new OrderDto
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            FirstName = order.FirstName,
            LastName = order.LastName,
            OrderDate = order.OrderDate,
            Email = order.Email,
            PhoneNumber = order.PhoneNumber,
            StreetAddress1 = order.StreetAddress1,
            StreetAddress2 = order.StreetAddress2,
            City = order.City,
            StateOrRegion = order.StateOrRegion,
            PostalCode = order.PostalCode,
            Country = order.Country
        };
    }
}