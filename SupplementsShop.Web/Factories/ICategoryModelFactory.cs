using SupplementsShop.Web.ViewModels;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Web.Factories;

public interface ICategoryModelFactory
{
    public CategoryViewModel PrepareCategoryViewModel(Category category);
    public IList<CategoryViewModel>? PrepareCategoryViewModels(IList<Category> categories);

    public CategoryProductsListModel PrepareCategoryProductsListModel(Category category,
        IPagedList<Product> productsPagedList);
}