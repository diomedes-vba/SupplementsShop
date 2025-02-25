using Microsoft.Extensions.DependencyInjection;
using SupplementsShop.Application.Services;

namespace SupplementsShop.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}