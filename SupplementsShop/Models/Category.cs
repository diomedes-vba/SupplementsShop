using System.ComponentModel.DataAnnotations;

namespace SupplementsShop.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Category>? ChildrenCategories { get; set; }
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public List<Product> Products { get; set; }
}