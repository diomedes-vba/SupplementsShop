using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;
using SupplementsShop.Infrastructure.Persistence;

namespace SupplementsShop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SupplementsShopContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<SupplementsShopContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager<SignInManager<User>>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();

        return services;
    }
}