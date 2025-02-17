using Microsoft.EntityFrameworkCore;
using SupplementsShop.Models;

namespace SupplementsShop.Data;

public class SupplementsShopContext : DbContext
{
    public SupplementsShopContext(DbContextOptions<SupplementsShopContext> options) : base(options) {}
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Company> Companies { get; set; }
}