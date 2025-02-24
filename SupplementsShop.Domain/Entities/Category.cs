namespace SupplementsShop.Domain.Entities;

public class Category
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string Slug { get; private set; }
    
    // Relationships
    public List<Product> Products { get; private set; } = new List<Product>();
    public int? ParentCategoryId { get; private set; }
    public Category? ParentCategory { get; private set; }
    public List<Category> ChildCategories { get; private set; } = new List<Category>();
    
    // Constructor for EF
    private Category() {}
    
    // Constructor with parameters
    public Category(string name, string slug, string? description = null, int? parentCategoryId = null)
    {
        Name = name;
        Slug = slug;
        Description = description;
        ParentCategoryId = parentCategoryId;
    }
}