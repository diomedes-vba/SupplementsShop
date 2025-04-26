namespace SupplementsShop.Web.ViewModels;

public class ProductDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Slug { get; set; }
    public int Sales { get; set; }
    public int CompanyId { get; set; }
    public int? Quantity { get; set; }
}