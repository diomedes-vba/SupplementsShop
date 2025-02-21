using System.ComponentModel.DataAnnotations;

namespace SupplementsShop.Models;

public class Category
{
    public int CategoryId { get; set; }
    [Required]
    public string CategoryName { get; set; }
    public string? CategoryDescription { get; set; }
    public List<Category>? ChildrenCategories { get; set; }
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public List<Product> CategoryProducts { get; set; }
    public string CategoryThug { get; set; }
}