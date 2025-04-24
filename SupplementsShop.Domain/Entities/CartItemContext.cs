namespace SupplementsShop.Domain.Entities;

public class CartItemContext
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public Product? Product { get; private set; }
    public int Quantity { get; private set; }
    public string UserId { get; private set; }

    public CartItemContext(int productId, int quantity, string userId)
    {
        ProductId = productId;
        Quantity = quantity;
        UserId = userId;
    }

    public void UpdateCartItemQuantity(int quantity)
    {
        Quantity = quantity;
    }

    public void IncreaseCartItemQuantity(int quantity)
    {
        Quantity += quantity;
    }
}