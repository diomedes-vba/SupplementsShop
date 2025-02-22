namespace SupplementsShop.Models;

public class Cart
{
    private List<CartItem> _items;
    public List<CartItem> Items => _items;

    public void AddItem(Product product, int quantity = 1)
    {
        var item = _items.FirstOrDefault(p => p.CartItemId == product.ProductId);
        
        if (item == null)
        {
            _items.Add(new CartItem 
            {
                CartItemId = product.ProductId, 
                CartItemName = product.ProductName, 
                CartItemPrice = product.ProductPrice, 
                CartItemQuantity = quantity
                
            });
        }
        else item.CartItemQuantity += quantity;
    }

    public void RemoveItem(int productId)
    {
        _items.RemoveAll(p => p.CartItemId == productId);
    }
    
    public float TotalPrice => _items.Sum(p => p.CartItemPrice * p.CartItemQuantity);

    public void Clear() => _items.Clear();
}