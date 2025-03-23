using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Factories;

public interface IProductModelFactory
{
    public IList<ProductDto>? PrepareProductDtos(IList<Product>? products);
}