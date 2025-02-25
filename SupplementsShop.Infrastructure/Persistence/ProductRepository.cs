using Microsoft.EntityFrameworkCore;
using SupplementsShop.Domain.Interfaces;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Infrastructure.Persistence;

public class ProductRepository : IProductRepository
{
    private readonly SupplementsShopContext _context;

    public ProductRepository(SupplementsShopContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetBySlugAsync(string slug)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Slug == slug);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}