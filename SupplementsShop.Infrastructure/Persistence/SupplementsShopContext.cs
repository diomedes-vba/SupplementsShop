using Microsoft.EntityFrameworkCore;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Infrastructure.Persistence;

public class SupplementsShopContext : DbContext
{
    public SupplementsShopContext(DbContextOptions<SupplementsShopContext> options) : base(options) { }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Company> Companies { get; set; }
}