namespace SupplementsShop.Domain.Models;

public class CartItem
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public string ImageUrl { get; private set; }
    
    public decimal TotalPrice => Price * Quantity;

    public CartItem(int id, string name, decimal price, int quantity, string imageUrl)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        ImageUrl = imageUrl;
    }
    
    public void IncreaseQuantity(int amount) => Quantity += amount;
    public void DecreaseQuantity(int amount) => Quantity -= amount;
}