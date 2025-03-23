using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Application.Services;

public interface IProductService
{
    Task<ProductDto> GetProductBySlugAsync(string slug);
    Task<ProductDto> GetProductByIdAsync(int productId);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<IPagedList<Product>?> GetProductListByCategoryIdAsync(int categoryId, int page = 0, int pageSize = int.MaxValue);
}