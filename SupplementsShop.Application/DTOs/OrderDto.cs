namespace SupplementsShop.Application.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string CustomerName { get; set; }
    public DateTime OrderDate { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    
    public bool IsPaid { get; set; }
    public bool IsShipped { get; set; }
}