using SupplementsShop.Web.ViewModels;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Models;

namespace SupplementsShop.Web.Factories;

public interface IOrderModelFactory
{
    OrderViewModel PrepareOrderViewModel(Order order);
    IList<OrderItemViewModel> PrepareOrderItemViewModels(IList<OrderItem> orderItems);
    OrderItemViewModel PrepareOrderItemViewModel(OrderItem orderItem);
    Order PrepareOrderFromViewModel(OrderViewModel orderViewModel);
    IList<OrderItem> PrepareOrderItemsFromCart(IList<CartItem> cartItems);
}