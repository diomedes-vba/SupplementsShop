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
    
    public bool ProductIsAvailable => ProductQuantity > 0;
    public string ProductSlug => $"{string.Join("-", ProductName.ToLower().Split(' '))}";
    public int ProductSales { get; set; } = 0;
}