namespace SupplementsShop.Domain.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public string Description { get; private set; }
    public string Slug { get; private set; }
    public int Sales { get; private set; }
    public string ImageUrl { get; private set; }
    
    // Relationships
    public int CompanyId { get; private set; }
    public Company Company { get; private set; }
    public List<Category> Categories { get; private set; } = new List<Category>();
    
    public bool IsAvailable => Quantity > 0;
    
    // Constructor for EF
    private Product() {}
    
    // Constructor with parametres
    public Product(string name, decimal price, int quantity, string description, int companyId, string slug, string imageUrl)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        Description = description;
        CompanyId = companyId;
        Slug = slug;
        ImageUrl = imageUrl;
    }
    
    // Business logic: Minus quantity when sold
    public void Sell(int amount)
    {
        Quantity -= amount;
        Sales += amount;
    }

    public void Restock(int amount)
    {
        Quantity += amount;
    }
    
}