using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Infrastructure.Identity;

public static class UserSeeder
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        
        string[] roles = { "Admin", "User" };

        // Ensuring roles existence
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        
        // Assigning Admin roles to specific emails
        var config = services.GetRequiredService<IConfiguration>();
        var adminEmail = config["SeedUsers:AdminEmail"];
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
        
        // Assigning User roles to all role-non-having individuals
        var users = userManager.Users.ToList();
        foreach (var user in users)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            if (!userRoles.Any())
            {
                await userManager.AddToRoleAsync(user, "User");
            }
        }
    }
}