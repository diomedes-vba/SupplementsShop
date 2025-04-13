namespace SupplementsShop.Domain.Entities;

public class OrderItem
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public string ImageUrl { get; private set; }
    
    public decimal TotalPrice => Price * Quantity;
    
    public int ProductId { get; private set; }
    public int OrderId { get; private set; }
    public Order Order { get; private set; }
    
    public OrderItem() {}

    public OrderItem(string name, decimal price, int quantity, string imageUrl, int productId)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        ImageUrl = imageUrl;
        ProductId = productId;
    }

    public void SetOrder(int orderId)
    {
        OrderId = orderId;
    }
}