namespace SupplementsShop.Models;

public class CartItem
{
    public int CartItemId { get; set; }
    public string CartItemName { get; set; }
    public float CartItemPrice { get; set; }
    public int CartItemQuantity { get; set; }
}