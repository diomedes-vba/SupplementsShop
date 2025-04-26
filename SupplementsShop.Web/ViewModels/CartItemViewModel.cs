namespace SupplementsShop.Web.ViewModels;

public class CartItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; }
    public string ProductNumber { get; set; }
    public int InventoryQuantity { get; set; }
    public decimal TotalPrice => Price * Quantity;
}