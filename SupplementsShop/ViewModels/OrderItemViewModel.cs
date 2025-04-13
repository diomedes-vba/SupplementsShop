namespace SupplementsShop.ViewModels;

public class OrderItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; }
    
    public decimal TotalPrice => Price * Quantity;
    
    public int ProductId { get; set; }
    public int OrderId { get; set; }
}