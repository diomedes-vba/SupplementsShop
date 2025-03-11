using Microsoft.EntityFrameworkCore;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Infrastructure.Persistence;

public class CategoryRepository : ICategoryRepository
{
    private readonly SupplementsShopContext _context;

    public CategoryRepository(SupplementsShopContext context)
    {
        _context = context;
    }

    public async Task<Category?> GetBySlugAsync(string slug, int page, int pageSize = 40)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Slug == slug);
        
        var pagedProducts = await _context.CategoryProducts
            .Where(cp => cp.CategoryId == category.Id)
            .OrderBy(cp => cp.CategoryId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(cp => cp.Product)
            .ToListAsync();

        category?.AddCategoryProducts(pagedProducts);
        return category;
    }
    
    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}