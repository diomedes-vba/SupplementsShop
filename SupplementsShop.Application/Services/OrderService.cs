using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;
using SupplementsShop.Domain.Models;

namespace SupplementsShop.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IInventoryApiClient _inventoryApiClient;
    private readonly IProductService _productService;

    public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, 
        IInventoryApiClient inventoryApiClient, IProductService productService)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _inventoryApiClient = inventoryApiClient;
        _productService = productService;
    }

    public async Task<int?> CreateOrderAsync(Order order, IList<OrderItem> orderItems)
    {
        var productNumbers = orderItems.Select(oi => oi.ProductNumber).ToArray();
        var productQuantities = await _productService.GetProductQuantityAsync(productNumbers);

        foreach (var orderItem in orderItems)
        {
            if (orderItem.Quantity > productQuantities[orderItem.ProductNumber])
                return null;
        }
        
        orderItems.Select(UpdateInventoryAsync);
        
        var orderNumber = _orderRepository.GetNextOrderNumberAsync();
        order.SetOrderNumber(orderNumber);
        order.SetOrderDate(DateTime.Now);
        
        await _orderRepository.AddAsync(order);

        await AddOrderItemsToOrderAsync(order, orderItems);

        return order.OrderNumber;
    }

    private async Task UpdateInventoryAsync(OrderItem orderItem)
    {
        var inventoryItem = await _inventoryApiClient.GetInventoryItemAsync(orderItem.ProductNumber);
        if (inventoryItem == null)
            throw new Exception($"Product {orderItem.ProductNumber} not found");

        inventoryItem.Quantity -= orderItem.Quantity;
        
        await _inventoryApiClient.UpdateInventoryItemAsync(inventoryItem);
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