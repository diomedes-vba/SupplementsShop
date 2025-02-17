namespace SupplementsShop.Models;

public class CategoriesRepository
{
    private static List<Category> _categories = new List<Category>()
    {
        new Category {Id = 1, Name = "Vitamins", Description = "Vitamins"}, 
        new Category {Id = 2, Name = "Minerals", Description = "Minerals"},
        new Category {Id = 3, Name = "Herbs", Description = "Herbs"}
    };

    public static void AddCategory(Category category)
    {
        var maxId = _categories.Max(x => x.Id);
        category.Id = maxId + 1;
        _categories.Add(category);
    }
    
    public static List<Category> GetCategories() => _categories;

    public static Category? GetCategoryById(int id)
    {
        var category = _categories.FirstOrDefault(x => x.Id == id);
        if (category != null)
            return new Category() { Id = category.Id, Name = category.Name, Description = category.Description };
        return null;
    }

    public static void UpdateCategory(int id, Category category)
    {
        if (id != category.Id) return;
        var categoryToUpdate = _categories.FirstOrDefault(x => x.Id == id);
        if (categoryToUpdate != null)
        {
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;
        }
    }

    public static void DeleteCategory(int id)
    {
        var categoryToDelete = _categories.FirstOrDefault(x => x.Id == id);
        if (categoryToDelete != null)
        {
            _categories.Remove(categoryToDelete);
        }
    }
}