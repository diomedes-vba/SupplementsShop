using Microsoft.Extensions.DependencyInjection;
using SupplementsShop.Application.Services;

namespace SupplementsShop.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddTransient<IEmailSenderService, EmailSenderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IImageService, ImageService>();

        return services;
    }
}