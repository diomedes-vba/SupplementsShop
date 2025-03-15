using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Models;

public class Cart
{
    private List<CartItem> _items = new List<CartItem>();
    public List<CartItem> Items => _items;

    public void AddItem(CartItem cartItem)
    {
        var item = _items.FirstOrDefault(ci => ci.Id == cartItem.Id);

        if (item == null)
        {
            _items.Add(cartItem);
        }
        else
        {
            item.IncreaseQuantity(cartItem.Quantity);
        }
    }

    public void UpdateItemQuantity(int productId, int quantity)
    {
        var item = _items.FirstOrDefault(p => p.Id == productId);

        item.UpdateQuantity(quantity);
    }
    
    public void RemoveItem(int productId) => _items.RemoveAll(p => p.Id == productId);
    
    public decimal TotalPrice => _items.Sum(p => p.TotalPrice);
    
    public void Clear() => _items.Clear();
}