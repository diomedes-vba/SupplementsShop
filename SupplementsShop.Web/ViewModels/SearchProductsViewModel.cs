namespace SupplementsShop.Web.ViewModels;

public class SearchProductsViewModel
{
    public string SearchTerm { get; set; }
    public IList<ProductCategoryViewModel>? Products { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalProducts { get; set; }
    public int PageSize { get; set; }
}