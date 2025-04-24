using SupplementsShop.ViewModels;
using SupplementsShop.Application.DTOs;
using SupplementsShop.Application.Services;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Factories;

public class CategoryModelFactory : ICategoryModelFactory
{
    private readonly ICategoryService _categoryService;
    private readonly IProductModelFactory _productModelFactory;

    public CategoryModelFactory(ICategoryService categoryService, IProductModelFactory productModelFactory)
    {
        _categoryService = categoryService;
        _productModelFactory = productModelFactory;
    }

    public CategoryViewModel PrepareCategoryViewModel(Category category)
    {
        return new CategoryViewModel
        {
            Id = category.Id,
            Description = category.Description,
            Name = category.Name,
            Slug = category.Slug
        };
    }

    public IList<CategoryViewModel>? PrepareCategoryViewModels(IList<Category>? categories)
    {
        return categories?.Select(PrepareCategoryViewModel).ToList();
    }

    public CategoryProductsListModel PrepareCategoryProductsListModel(Category category,
        IPagedList<Product> productsPagedList)
    {
        return new CategoryProductsListModel
        {
            CategoryId = category.Id,
            CategoryName = category.Name,
            CategorySlug = category.Slug,
            ChildCategories = PrepareCategoryViewModels(category.ChildCategories),
            Products = _productModelFactory.PrepareProductCategoryViewModels(productsPagedList),
            CurrentPage = productsPagedList.PageIndex,
            TotalPages = productsPagedList.TotalPages,
            TotalProducts = productsPagedList.TotalCount,
            PageSize = productsPagedList.PageSize
        };
    }
}