namespace SupplementsShop.Domain.Entities;

public class Order
{
    public int Id { get; private set; }
    public int? OrderNumber { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime OrderDate { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    
    // Address info
    public string StreetAddress1 { get; private set; }
    public string StreetAddress2 { get; private set; }
    public string City { get; private set; }
    public string StateOrRegion { get; private set; }
    public string PostalCode { get; private set; }
    public string Country { get; private set; }

    public List<OrderItem> Items { get; private set; }
    public int UserId { get; private set; }
    public User User { get; private set; }
    
    public bool IsPaid { get; private set; }
    public bool IsShipped { get; private set; }
    
    // EF constructor
    public Order() {}
    
    // Constructor with parameters
    public Order(int? orderNumber, string firstName, string lastName, DateTime orderDate, string email,
        string phoneNumber, string streetAddress1, string streetAddress2, string city, string stateOrRegion,
        string postalCode, string country)
    {
        OrderNumber = orderNumber;
        FirstName = firstName;
        LastName = lastName;
        OrderDate = orderDate;
        Email = email;
        PhoneNumber = phoneNumber;
        StreetAddress1 = streetAddress1;
        StreetAddress2 = streetAddress2;
        City = city;
        StateOrRegion = stateOrRegion;
        PostalCode = postalCode;
        Country = country;
    }

    public void AddItems(List<OrderItem> items)
    {
        Items = items;
    }
}