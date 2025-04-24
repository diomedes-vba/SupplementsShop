using FluentAssertions;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Tests;

public class CartItemContextTests
{
    [Fact]
    public void Ctor_SetsPropertiesCorrectly()
    {
        int productId = 42;
        int quantity = 3;
        string userId = "user-123";
        
        var cartItem = new CartItemContext(productId, quantity, userId);
        
        cartItem.ProductId.Should().Be(productId);
        cartItem.Quantity.Should().Be(quantity);
        cartItem.UserId.Should().Be(userId);
        cartItem.Product.Should().BeNull();
    }

    [Fact]
    public void UpdateCartItemQuantity_ChangesQuantityCorrectly()
    {
        var cartItem = new CartItemContext(1, 5, "u1");
        
        cartItem.UpdateCartItemQuantity(2);
        
        cartItem.Quantity.Should().Be(2);
    }

    [Fact]
    public void IncreaseCartItemQuantity_AddsToQuantityCorrectly()
    {
        var cartItem = new CartItemContext(1, 5, "u1");
        
        cartItem.IncreaseCartItemQuantity(1);
        
        cartItem.Quantity.Should().Be(6);
    }
}