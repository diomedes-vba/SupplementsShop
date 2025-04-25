namespace SupplementsShop.Domain.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string ProductNumber { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    public string Slug { get; private set; }
    public int Sales { get; private set; }
    public string ImageUrl { get; private set; }
    
    // Relationships
    public int CompanyId { get; private set; }
    public Company Company { get; private set; }
    public List<CategoryProduct> CategoryProducts { get; private set; } = new List<CategoryProduct>();
    
    // Constructor for EF
    private Product() {}
    
    // Constructor with parameters
    public Product(string name, string productNumber, decimal price, string description, int companyId, string slug, string imageUrl)
    {
        Name = name;
        ProductNumber = productNumber;
        Price = price;
        Description = description;
        CompanyId = companyId;
        Slug = slug;
        ImageUrl = imageUrl;
    }
    
    public void UpdateSlug(string slug)
    {
        Slug = slug;
    }
    
}