using SupplementsShop.Web.ViewModels;
using SupplementsShop.Application.Services;
using SupplementsShop.Domain.Models;

namespace SupplementsShop.Web.Factories;

public class CartModelFactory : ICartModelFactory
{
    public CartViewModel PrepareCartViewModel(Cart? cart)
    {
        return new CartViewModel
        {
            Items = PrepareCartItems(cart?.Items),
            TotalPrice = cart?.TotalPrice ?? 0
        };
    }

    private  IList<CartItemViewModel>? PrepareCartItems(IList<CartItem>? cartItems)
    {
        return cartItems?.Select(PrepareCartItemViewModel).ToList();
    }

    private CartItemViewModel PrepareCartItemViewModel(CartItem? cartItem)
    {
        return new CartItemViewModel
        {
            Id = cartItem.Id,
            Name = cartItem.Name,
            Price = cartItem.Price,
            Quantity = cartItem.Quantity,
            ImageUrl = cartItem.ImageUrl,
            ProductNumber = cartItem.ProductNumber
        };
    }
}