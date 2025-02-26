using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Models;

public class Cart
{
    private List<CartItem> _items = new List<CartItem>();
    public List<CartItem> Items => _items;

    public void AddItem(Product product, int quantity)
    {
        var item = _items.FirstOrDefault(p => p.Id == product.Id);

        if (item == null)
        {
            _items.Add(new CartItem(
                product.Id,
                product.Name,
                product.Price,
                quantity,
                product.ImageUrl
                ));
        }
        else
        {
            item.IncreaseQuantity(quantity);
        }
    }
    
    public void RemoveItem(int productId) => _items.RemoveAll(p => p.Id == productId);
    
    public decimal TotalPrice => _items.Sum(p => p.TotalPrice);
    
    public void Clear() => _items.Clear();
}