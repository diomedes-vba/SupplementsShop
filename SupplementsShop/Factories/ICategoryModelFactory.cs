using SupplementsShop.Application.DTOs;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;
using SupplementsShop.ViewModels;

namespace SupplementsShop.Factories;

public interface ICategoryModelFactory
{
    public CategoryViewModel PrepareCategoryViewModel(Category category);
    public IList<CategoryViewModel>? PrepareCategoryViewModels(IList<Category> categories);

    public CategoryProductsListModel PrepareCategoryProductsListModel(Category category,
        IPagedList<Product> productsPagedList);
}