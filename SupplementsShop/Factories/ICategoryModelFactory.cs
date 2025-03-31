using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;
using SupplementsShop.ViewModels;

namespace SupplementsShop.Factories;

public interface ICategoryModelFactory
{
    public CategoryDto PrepareCategoryDto(Category category);
    public IList<CategoryDto>? PrepareCategoryDtos(IList<Category> categories);

    public CategoryProductsListModel PrepareCategoryProductsListModel(Category category,
        IPagedList<Product> productsPagedList);
}