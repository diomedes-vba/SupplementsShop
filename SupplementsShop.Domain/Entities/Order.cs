using System.ComponentModel.DataAnnotations;

namespace SupplementsShop.Domain.Entities;

public class Order
{
    public int Id { get; private set; }
    public int OrderNumber { get; private set; }
    [Required]
    public string FirstName { get; private set; }
    [Required]
    public string LastName { get; private set; }
    public DateTime OrderDate { get; private set; }
    [Required]
    public string Email { get; private set; }
    [Required]
    public string PhoneNumber { get; private set; }
    
    // Address info
    [Required]
    public string StreetAddress1 { get; private set; }
    public string? StreetAddress2 { get; private set; }
    [Required]
    public string City { get; private set; }
    public string? StateOrRegion { get; private set; }
    [Required]
    public string PostalCode { get; private set; }
    [Required]
    public string Country { get; private set; }

    public IList<OrderItem> Items { get; private set; }
    public string UserId { get; private set; }
    public User User { get; private set; }
    
    public bool IsPaid { get; private set; }
    public bool IsShipped { get; private set; }
    
    // EF constructor
    public Order() {}
    
    // Constructor with parameters
    public Order(string firstName, string lastName, string email,
        string phoneNumber, string streetAddress1, string? streetAddress2, string city, string? stateOrRegion,
        string postalCode, string country, string userId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        StreetAddress1 = streetAddress1;
        StreetAddress2 = streetAddress2;
        City = city;
        StateOrRegion = stateOrRegion;
        PostalCode = postalCode;
        Country = country;
        UserId = userId;
    }

    public void AddItems(IList<OrderItem> items)
    {
        Items = items;
    }

    public void SetOrderNumber(int orderNumber)
    {
        OrderNumber = orderNumber;
    }

    public void SetOrderDate(DateTime orderDate)
    {
        OrderDate = orderDate;
    }
}