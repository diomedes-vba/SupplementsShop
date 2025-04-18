using System.ComponentModel.DataAnnotations;

namespace SupplementsShop.ViewModels;

public class OrderViewModel
{
    public int Id { get; set; }
    public int? OrderNumber { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public DateTime OrderDate { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    
    // Address properties
    [Required]
    public string StreetAddress1 { get; set; }
    public string? StreetAddress2 { get; set; }
    [Required]
    public string City { get; set; }
    public string? StateOrRegion { get; set; }
    [Required]
    public string PostalCode { get; set; }
    [Required]
    public string Country { get; set; }
    
    public IList<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
    public string UserId { get; set; }
    
    public bool IsPaid { get; set; }
    public bool IsShipped { get; set; }
}