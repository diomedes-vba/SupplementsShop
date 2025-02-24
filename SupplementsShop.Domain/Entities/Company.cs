namespace SupplementsShop.Domain.Entities;

public class Company
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string Address { get; private set; }
    public string Slug { get; private set; }
    
    // Relationships
    public List<Product> Products { get; private set; } = new List<Product>();
    
    // Constructor for EF
    private Company() {}
    
    // Constructor with parameters
    public Company(string name, string? description, string address, string slug)
    {
        Name = name;
        Description = description;
        Address = address;
        Slug = slug;
    }
}