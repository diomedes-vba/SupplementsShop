using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Models;
using SupplementsShop.ViewModels;

namespace SupplementsShop.Factories;

public interface IOrderModelFactory
{
    OrderViewModel PrepareOrderViewModel(Order order);
    IList<OrderItemViewModel> PrepareOrderItemViewModels(IList<OrderItem> orderItems);
    OrderItemViewModel PrepareOrderItemViewModel(OrderItem orderItem);
    Order PrepareOrderFromViewModel(OrderViewModel orderViewModel);
    IList<OrderItem> PrepareOrderItemsFromCart(IList<CartItem> cartItems);
}