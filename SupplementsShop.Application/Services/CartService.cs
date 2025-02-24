using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;
using SupplementsShop.Domain.Models;

namespace SupplementsShop.Application.Services;

public class CartService : ICartService
{
    private readonly IProductRepository _productRepository;

    public CartService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async void AddToCart(Cart cart, int productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) return;
        
        cart.AddItem(product, quantity);
    }

    public void RemoveFromCart(Cart cart, int productId)
    {
        cart.RemoveItem(productId);
    }

    public decimal GetTotalPrice(Cart cart)
    {
        return cart.TotalPrice;
    }

    public void ClearCart(Cart cart)
    {
        cart.Clear();
    }
}