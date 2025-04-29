using SupplementsShop.Web.ViewModels;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Web.Factories;

public interface ICategoryModelFactory
{
    CategoryViewModel PrepareCategoryViewModel(Category category);
    
    IList<CategoryViewModel>? PrepareCategoryViewModels(IList<Category> categories);

    CategoryProductsListModel PrepareCategoryProductsListModel(Category category,
        IPagedList<Product> productsPagedList);

    HomeCategoryViewModel PrepareHomeCategoryViewModel(Category category);

    IList<HomeCategoryViewModel>? PrepareHomeCategoryViewModels(IList<Category>? categories);
}