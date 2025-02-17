namespace SupplementsShop.Models;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public float ProductPrice { get; set; }
    public int ProductQuantity { get; set; }
    public string ProductDescription { get; set; } = string.Empty;
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public List<Category> Categories { get; set; }
    
    public bool IsAvailable => ProductQuantity > 0;
}