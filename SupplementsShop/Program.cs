using System.Net.Mime;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

// Route for single product
app.MapControllerRoute(
    name: "ProductDetails",
    pattern: "Product/{slug}",
    defaults: new { controller = "Product", action = "Details" });

// Route for single category
app.MapControllerRoute(
    name: "CategoryProducts",
    pattern: "Category/{slug}",
    defaults: new { controller = "Category", action = "Products" });

// Route for single company
app.MapControllerRoute(
    name: "CompanyDetails",
    pattern: "Company/{slug}",
    defaults: new { controller = "Company", action = "Details" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

