namespace SupplementsShop.Application.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime OrderDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    
    // Address properties
    public string StreetAddress1 { get; set; }
    public string StreetAddress2 { get; set; }
    public string City { get; set; }
    public string StateOrRegion { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    
    public List<OrderItemDto> OrderItems { get; set; }
    
    public bool IsPaid { get; set; }
    public bool IsShipped { get; set; }
}