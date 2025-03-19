using SupplementsShop.Application.DTOs;

namespace SupplementsShop.ViewModels;

public class ProductsListModel
{
    public List<ProductDto>? Products { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalProducts { get; set; }
    public int PageSize { get; set; }
}