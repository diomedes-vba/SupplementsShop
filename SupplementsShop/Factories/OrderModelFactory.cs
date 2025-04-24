using SupplementsShop.ViewModels;
using SupplementsShop.Application.Services;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Models;

namespace SupplementsShop.Factories;

public class OrderModelFactory : IOrderModelFactory
{
    private readonly IOrderService _orderService;

    public OrderModelFactory(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    public OrderViewModel PrepareOrderViewModel(Order order)
    {
        return new OrderViewModel
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
            Country = order.Country,
            OrderItems = PrepareOrderItemViewModels(order.Items),
            UserId = order.UserId,
            IsPaid = order.IsPaid,
            IsShipped = order.IsShipped
        };
    }

    public IList<OrderItemViewModel> PrepareOrderItemViewModels(IList<OrderItem> orderItems)
    {
        var orderItemViewModels = orderItems.Select(PrepareOrderItemViewModel).ToList();
        return orderItemViewModels;
    }

    public OrderItemViewModel PrepareOrderItemViewModel(OrderItem orderItem)
    {
        return new OrderItemViewModel
        {
            Id = orderItem.Id,
            Name = orderItem.Name,
            Price = orderItem.Price,
            Quantity = orderItem.Quantity,
            ImageUrl = orderItem.ImageUrl,
            ProductId = orderItem.ProductId,
            OrderId = orderItem.OrderId
        };
    }

    public Order PrepareOrderFromViewModel(OrderViewModel orderModel)
    {
        var order = new Order
        (
            firstName: orderModel.FirstName,
            lastName: orderModel.LastName,
            email: orderModel.Email,
            phoneNumber: orderModel.PhoneNumber,
            streetAddress1: orderModel.StreetAddress1,
            streetAddress2: orderModel.StreetAddress2,
            city: orderModel.City,
            stateOrRegion: orderModel.StateOrRegion,
            postalCode: orderModel.PostalCode,
            country: orderModel.Country,
            userId: orderModel.UserId
        );
        return order;
    }

    public IList<OrderItem> PrepareOrderItemsFromCart(IList<CartItem> cartItems)
    {
        return cartItems.Select(ci => new OrderItem(
            name: ci.Name,
            price: ci.Price,
            quantity: ci.Quantity,
            imageUrl: ci.ImageUrl,
            productId: ci.Id,
            productNumber: ci.ProductNumber))
            .ToList();
    }
}