using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Infrastructure.Persistence;

public class SupplementsShopContext : IdentityDbContext<User>
{
    public SupplementsShopContext(DbContextOptions<SupplementsShopContext> options) : base(options) { }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryProduct> CategoryProducts { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<CartItemContext> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Primary key for the join entity
        builder.Entity<CategoryProduct>()
            .HasKey(cp => new { cp.CategoryId, cp.ProductId });
        
        // Relationship: Category -> CategoryProduct
        builder.Entity<CategoryProduct>()
            .HasOne(cp => cp.Category)
            .WithMany(c => c.CategoryProducts)
            .HasForeignKey(cp => cp.CategoryId);
        
        // Relationship: Product -> CategoryProduct
        builder.Entity<CategoryProduct>()
            .HasOne(cp => cp.Product)
            .WithMany(p => p.CategoryProducts)
            .HasForeignKey(cp => cp.ProductId);
        
        builder.Entity<OrderItem>()
            .Property(o => o.Id)
            .ValueGeneratedOnAdd();
    }
}