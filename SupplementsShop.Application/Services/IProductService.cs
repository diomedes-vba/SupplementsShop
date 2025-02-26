using SupplementsShop.Application.DTOs;

namespace SupplementsShop.Application.Services;

public interface IProductService
{
    Task<ProductDto> GetProductBySlugAsync(string slug);
    Task<ProductDto> GetProductByIdAsync(int productId);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
}