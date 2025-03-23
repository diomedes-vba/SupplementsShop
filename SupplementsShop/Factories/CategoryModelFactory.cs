using SupplementsShop.Application.DTOs;
using SupplementsShop.Application.Services;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;
using SupplementsShop.ViewModels;

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

    public CategoryDto PrepareCategoryDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Description = category.Description,
            Name = category.Name,
            Slug = category.Slug
        };
    }

    public IList<CategoryDto>? PrepareCategoryDtos(IList<Category>? categories)
    {
        return categories?.Select(PrepareCategoryDto).ToList();
    }

    public CategoryProductsListModel PrepareCategoryProductsListModel(Category category,
        IPagedList<Product>? productsPagedList)
    {
        return new CategoryProductsListModel
        {
            CategoryId = category.Id,
            CategoryName = category.Name,
            CategorySlug = category.Slug,
            ChildCategories = PrepareCategoryDtos(category.ChildCategories),
            Products = _productModelFactory.PrepareProductDtos(productsPagedList),
            CurrentPage = productsPagedList.PageIndex,
            TotalPages = productsPagedList.TotalPages,
            TotalProducts = productsPagedList.TotalCount,
            PageSize = productsPagedList.PageSize
        };
    }
}