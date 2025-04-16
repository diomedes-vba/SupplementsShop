namespace SupplementsShop.Domain.Models;

public class CartItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; }
    public string ProductNumber { get; set; }
    
    public decimal TotalPrice => Price * Quantity;


    public void IncreaseQuantity(int amount) => Quantity += amount;
    public void DecreaseQuantity(int amount) => Quantity -= amount;
    public void UpdateQuantity(int newQuantity) => Quantity = newQuantity;
}