using System.ComponentModel.DataAnnotations;

namespace SupplementsShop.Models;

public class Category
{
    public int CategoryId { get; set; }
    [Required]
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryDescription { get; set; } = string.Empty;
    public List<Category>? ChildrenCategories { get; set; }
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public List<Product> CategoryProducts { get; set; }
    public string CategorySlug => $"{string.Join("-", CategoryName.ToLower().Split(' '))}";
}