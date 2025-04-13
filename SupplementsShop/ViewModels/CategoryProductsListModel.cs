using SupplementsShop.Application.DTOs;

namespace SupplementsShop.ViewModels;

public class CategoryProductsListModel
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategorySlug { get; set; }
    public IList<CategoryViewModel>? ChildCategories { get; set; }
    public IList<ProductViewModel>? Products { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalProducts { get; set; }
    public int PageSize { get; set; }
}