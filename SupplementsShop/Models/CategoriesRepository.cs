namespace SupplementsShop.Models;

public class CategoriesRepository
{
    private static List<Category> _categories = new List<Category>()
    {
        new Category {CategoryId = 1, CategoryName = "Vitamins", CategoryDescription = "Vitamins"}, 
        new Category {CategoryId = 2, CategoryName = "Minerals", CategoryDescription = "Minerals"},
        new Category {CategoryId = 3, CategoryName = "Herbs", CategoryDescription = "Herbs"}
    };

    public static void AddCategory(Category category)
    {
        var maxId = _categories.Max(x => x.CategoryId);
        category.CategoryId = maxId + 1;
        _categories.Add(category);
    }
    
    public static List<Category> GetCategories() => _categories;

    public static Category? GetCategoryById(int id)
    {
        var category = _categories.FirstOrDefault(x => x.CategoryId == id);
        if (category != null)
            return new Category() { CategoryId = category.CategoryId, CategoryName = category.CategoryName, CategoryDescription = category.CategoryDescription };
        return null;
    }

    public static void UpdateCategory(int id, Category category)
    {
        if (id != category.CategoryId) return;
        var categoryToUpdate = _categories.FirstOrDefault(x => x.CategoryId == id);
        if (categoryToUpdate != null)
        {
            categoryToUpdate.CategoryName = category.CategoryName;
            categoryToUpdate.CategoryDescription = category.CategoryDescription;
        }
    }

    public static void DeleteCategory(int id)
    {
        var categoryToDelete = _categories.FirstOrDefault(x => x.CategoryId == id);
        if (categoryToDelete != null)
        {
            _categories.Remove(categoryToDelete);
        }
    }
}