using Microsoft.EntityFrameworkCore;
using SupplementsShop.Domain.Interfaces;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Infrastructure.Extensions;

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

    public async Task<IPagedList<Product>?> GetByCategoryIdAsync(int categoryId, int page, int pageSize = int.MaxValue)
    {
        var query = _context.CategoryProducts
            .Where(cp => cp.CategoryId == categoryId)
            .OrderBy(cp => cp.CategoryId)
            .Include(cp => cp.Product);

        var productQuery = query.Select(cp => cp.Product);

        var pagedProducts = await productQuery.ToPagedListAsync(page, pageSize);
        return pagedProducts;
    }

    public async Task<IList<Product>?> GetAllAsync()
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

    public async Task<IList<int>> GetCategoryIdsForProductAsync(int productId)
    {
        var categoryIds = await _context.CategoryProducts
            .Where(cp => cp.ProductId == productId)
            .Select(cp => cp.CategoryId)
            .ToListAsync();
        
        return categoryIds;
    }

    public async Task<IPagedList<Product>?> SearchAsync(string searchTerm, int page, int pageSize)
    {
        IQueryable<Product> productsQuery = _context.Products;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            productsQuery = productsQuery
                .Where(p => EF.Functions.Like(p.Name, $"%{searchTerm}%"));
        }

        var products = await productsQuery.OrderBy(p => p.Name).ToPagedListAsync(page, pageSize);

        return products;
    }
}