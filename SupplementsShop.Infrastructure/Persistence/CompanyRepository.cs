using Microsoft.EntityFrameworkCore;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Infrastructure.Persistence;

public class CompanyRepository : ICompanyRepository
{
    private readonly SupplementsShopContext _context;

    public CompanyRepository(SupplementsShopContext context)
    {
        _context = context;
    }

    public async Task<Company?> GetBySlugAsync(string slug)
    {
        return await _context.Companies.FirstOrDefaultAsync(p => p.Slug == slug);
    }
    
    public async Task<List<Company>> GetAllAsync()
    {
        return await _context.Companies.ToListAsync();
    }

    public async Task AddAsync(Company company)
    {
        await _context.Companies.AddAsync(company);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Company company)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(p => p.Id == id);
        if (company != null)
        {
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }
    }
}