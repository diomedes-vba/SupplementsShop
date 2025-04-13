namespace SupplementsShop.ViewModels;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Slug { get; set; }
    
    public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
}