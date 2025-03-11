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

    public async Task CreateOrderAsync(OrderDto orderDto, CartDto cart)
    {
        var orderNumber = await _orderRepository.GetNextOrderNumberAsync();
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
            orderDto.Country);
        
        await _orderRepository.AddAsync(order);

        await AddOrderItemsToOrderAsync(order, cart);
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
}