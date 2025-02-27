using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SupplementsShop.Infrastructure.Persistence;

public class SupplementsShopContextFactory : IDesignTimeDbContextFactory<SupplementsShopContext>
{
    public SupplementsShopContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../SupplementsShop"))
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
        
        var optionsBuilder = new DbContextOptionsBuilder<SupplementsShopContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnectionString");
        
        optionsBuilder.UseSqlServer(connectionString);
        
        return new SupplementsShopContext(optionsBuilder.Options);
    }
}