namespace SupplementsShop.Domain.Entities;

public class Order
{
    public int Id { get; private set; }
    public int OrderNumber { get; private set; }
    public string CustomerName { get; private set; }
    public DateTime OrderDate { get; private set; }
    public string Address { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public List<OrderItem> Items { get; private set; }
    
    public bool IsPaid { get; private set; }
    public bool IsShipped { get; private set; }
}