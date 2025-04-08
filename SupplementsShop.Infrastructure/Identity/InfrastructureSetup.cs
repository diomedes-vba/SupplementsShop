namespace SupplementsShop.Infrastructure.Identity;

public static class InfrastructureSetup
{
    public static async Task InitializeInfrastructureAsync(this IServiceProvider serviceProvider)
    {
        await AppDbInitializer.SeedRolesAsync(serviceProvider);
    }
}