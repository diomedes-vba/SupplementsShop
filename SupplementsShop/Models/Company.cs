namespace SupplementsShop.Models;

public class Company
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string CompanyDescription { get; set; }
    public string CompanyAddress { get; set; }
    public List<Product> Products { get; set; }
}