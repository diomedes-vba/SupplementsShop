using Microsoft.EntityFrameworkCore;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;
using SupplementsShop.Infrastructure.Extensions;

namespace SupplementsShop.Infrastructure.Persistence;

public class CategoryRepository : ICategoryRepository
{
    private readonly SupplementsShopContext _context;

    public CategoryRepository(SupplementsShopContext context)
    {
        _context = context;
    }

    public async Task<Category?> GetBySlugAsync(string slug)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Slug == slug);
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
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

    public int GetProductCountForCategory(int categoryId)
    {
        var productCount = _context.CategoryProducts.Count(cp => cp.CategoryId == categoryId);
        return productCount;
    }
}