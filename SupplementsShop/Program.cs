using Microsoft.AspNetCore.Authentication.Cookies;
using SupplementsShop.Infrastructure;
using SupplementsShop.Application;
using Hangfire;

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

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

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

