using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SupplementsShop.Infrastructure;
using SupplementsShop.Application;
using SupplementsShop.Application.Services;
using SupplementsShop.Infrastructure.Data;
using SupplementsShop.Web.Factories;
using SupplementsShop.Infrastructure.Identity;
using SupplementsShop.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddApplicationServices();
var webRootPath = builder.Environment.WebRootPath;
builder.Services.AddScoped<IImageService>(_ => new ImageService(webRootPath));

builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7199/");
});

builder.Services.AddHttpClient<IInventoryApiClient, InventoryApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7199/");
});

builder.Services.AddScoped<ICategoryModelFactory, CategoryModelFactory>();
builder.Services.AddScoped<IProductModelFactory, ProductModelFactory>();
builder.Services.AddScoped<ICartModelFactory, CartModelFactory>();
builder.Services.AddScoped<IOrderModelFactory, OrderModelFactory>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<SupplementsShopContext>();
    await DbInitializer.InitializeAsync(ctx);
}

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.InitializeInfrastructureAsync();
    var services = scope.ServiceProvider;
    await UserSeeder.SeedRolesAndAdminAsync(services);
}

// Route for single product
app.MapControllerRoute(
    name: "ProductDetails",
    pattern: "Product/{slug}",
    defaults: new { controller = "Product", action = "Details" });

// Route for single category
app.MapControllerRoute(
    name: "CategoryProducts",
    pattern: "Category/{slug}",
    defaults: new { controller = "Category", action = "CategoryPage" });

// Route for single company
app.MapControllerRoute(
    name: "CompanyDetails",
    pattern: "Company/{slug}",
    defaults: new { controller = "Company", action = "Details" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

