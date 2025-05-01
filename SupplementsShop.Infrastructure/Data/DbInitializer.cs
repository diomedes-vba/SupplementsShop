using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Infrastructure.Persistence;

namespace SupplementsShop.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<SupplementsShopContext>();
        await context.Database.MigrateAsync();

        if (!await context.Categories.AnyAsync())
        {
            var categorySamples = new[]
            {
                new Category(name: "Vitamins", description: "Vitamins category", slug: "vitamins"),
                new Category(name: "Vitamin B", description: "B Vitamins category", slug: "vitamin-b",
                    parentCategoryId: 1),
                new Category(name: "Vitamin B1", description: "Vitamin B1 category", slug: "vitamin-b1",
                    parentCategoryId: 2)
            };
            
            context.Categories.AddRange(categorySamples);
            await context.SaveChangesAsync();
        }

        if (!await context.Companies.AnyAsync())
        {
            var companySample = new Company(name: "NOW Foods", description: "NOW foods", slug: "now-foods", address: "244 Knollwood Drive, Bloomingdale, IL 60108");
            
            await context.Companies.AddAsync(companySample);
            await context.SaveChangesAsync();
        }

        if (!await context.Products.AnyAsync())
        {
            var productSamples = new[]
            {
                new Product(
                    name: "NOW Foods, Vitamin D-3, High Potency, 2,000 IU, 120 Softgels",
                    productNumber: "SKU-1000",
                    price: 15.28m,
                    description:
                    "NOW\u00ae Vitamin D-3 softgels supply this key vitamin in a highly absorbable liquid softgel form. Vitamin D is normally obtained from the diet or produced by the skin from the ultraviolet energy of the sun. However, it is not abundant in food. As more people avoid sun exposure, Vitamin D supplementation becomes even more necessary to ensure that your body receives an adequate supply.",
                    companyId: 1,
                    slug: "now-foods-vitamin-d-3-high-potency-2-000-iu-120-softgels",
                    imageUrl: "/images/products/now-foods-vitamin-d-3-high-potency-2-000-iu-120-softgels.jpg"),

                new Product(
                    name: "NOW Foods, Vitamin A, 10,000 IU, 100 Softgels",
                    productNumber: "SKU-1001",
                    price: 15.73m,
                    description:
                    "Vitamin A is essential for the maintenance of the tissues that line the internal and external surfaces of the body, including the eyes, skin, respiratory, GI and urinary tracts.",
                    companyId: 1,
                    slug: "now-foods-vitamin-a-10-000-iu-100-softgels",
                    imageUrl: "/images/products/now-foods-vitamin-a-10-000-iu-100-softgels.jpg"),

                new Product(
                    name: "NOW Foods, C-1000, 250 Tablets",
                    productNumber: "SKU-1002",
                    price: 63.65m,
                    description:
                    "Vitamin C is a water soluble nutrient well known for its vital role in the immune system. Vitamin C is also necessary for the production of collagen (a structural protein in connective tissue) and is therefore important for skin, bone, and joint health. Vitamin C is needed for amino acid metabolism, neurotransmitter synthesis, and the utilization of many nutrients, such as folic acid and iron. It is also a highly effective antioxidant that can help maintain healthy tissues by neutralizing free radicals generated during normal metabolism and exposure to environmental stressors. This product was specially formulated to provide a sustained release of vitamin C and includes rose hips as an added source of vitamin C.",
                    companyId: 1,
                    slug: "now-foods-c-1000-250-tablets",
                    imageUrl: "/images/products/now-foods-c-1000-250-tablets.jpg"),

                new Product(
                    name: "NOW Foods, Niacinamide, 500 mg, 100 Veg Capsules",
                    productNumber: "SKU-1003",
                    price: 24.06m,
                    description:
                    "Niacinamide (Vitamin B-3) is a form of Niacin, a water soluble B-Vitamin. It is a derivative of Niacin that does not cause the \"flush\" normally associated with taking high doses of Niacin. Niacinamide is readily converted into the bioactive forms of Niacin, NAD+, NADH, NADP, and NADPH, which are vital cofactors in cellular energy production and are critical for the maintenance of DNA stability.",
                    companyId: 1,
                    slug: "now-foods-niacinamide-500-mg-100-veg-capsules",
                    imageUrl: "/images/products/now-foods-niacinamide-500-mg-100-veg-capsules.jpg"),

                new Product(
                    name: "NOW Foods, Mega D-3 & MK-7, 120 Capsules",
                    productNumber: "SKU-1004",
                    price: 87.74m,
                    description:
                    "NOW\u00ae Mega D-3 & MK-7 combines vitamins D-3 and K-2, two nutrients extensively researched for their roles in the health of bones, teeth and the cardiovascular system. Vitamin D-3 works with calcium to support bone health, and more recent studies indicate it’s important for immune system health as well. MenaQ7\u00ae MK-7 is a unique soy-free, bioavailable form of vitamin K-2 that plays a critical role in arterial health through its ability to support proper calcium metabolism in blood vessels and arteries. Vitamin K-2 is also important for the formation of strong, healthy bones.",
                    companyId: 1,
                    slug: "now-foods-mega-d-3-&-mk-7-120-capsules",
                    imageUrl: "/images/products/now-foods-mega-d-3-&-mk-7-120-capsules.jpg"),

                new Product(
                    name: "NOW Foods, MK-7 Vitamin K-2 , 100 mcg, 120 Veg Capsules",
                    productNumber: "SKU-1005",
                    price: 76.60m,
                    description:
                    "Vitamin K is well known for its role in the synthesis of a number of blood coagulation factors and is also important for the formation of strong, healthy bones. MenaQ7\u00ae MK-7 is a unique soy-free, bioavailable form of vitamin K-2 that plays a critical role in arterial health through its ability to support proper calcium metabolism in blood vessels and arteries.",
                    companyId: 1,
                    slug: "now-foods-mk-7-vitamin-k-2--100-mcg-120-veg-capsules",
                    imageUrl: "/images/products/now-foods-mk-7-vitamin-k-2--100-mcg-120-veg-capsules.jpg"),

                new Product(
                    name: "NOW Foods, B-1, 100 mg, 100 Tablets",
                    productNumber: "SKU-1006",
                    price: 18.62m,
                    description:
                    "Vitamin B-1, also know as Thiamin, is a member of the B-Vitamin family that is a naturally found in cereal grains, beans, nuts, eggs, and meats. Thiamin is involved in numerous body functions, including nervous system and muscle functioning, the flow of electrolytes in and out of nerve and muscle cells, carbohydrate metabolism, and the production of hydrochloric acid, which is necessary for proper digestion.",
                    companyId: 1,
                    slug: "now-foods-b-1-100-mg-100-tablets",
                    imageUrl: "/images/products/now-foods-b-1-100-mg-100-tablets.jpg"),

                new Product(
                    name: "NOW Foods, Vitamin K-2, 100 mcg, 100 Veg Capsules",
                    productNumber: "SKU-1007",
                    price: 33.02m,
                    description:
                    "Although Vitamin K is historically known for its role in normal blood clotting function, we now know that Vitamin K is also essential to bone, cardiovascular, and nervous system health. As a biologically active form of Vitamin K, K-2 is important for the formation of healthy, strong bone matrix. Vitamin K's role in arterial health revolves around its ability to support proper calcium metabolism in vascular structures.",
                    companyId: 1,
                    slug: "now-foods-vitamin-k-2-100-mcg-100-veg-capsules",
                    imageUrl: "/images/products/now-foods-vitamin-k-2-100-mcg-100-veg-capsules.jpg")
            };
            
            context.Products.AddRange(productSamples);
            await context.SaveChangesAsync();
        }

        if (!await context.CategoryProducts.AnyAsync())
        {
            var categoryProductSamples = new[]
            {
                new CategoryProduct { CategoryId = 1, ProductId = 1 },
                new CategoryProduct { CategoryId = 1, ProductId = 2 },
                new CategoryProduct { CategoryId = 1, ProductId = 3 },
                new CategoryProduct { CategoryId = 1, ProductId = 4 },
                new CategoryProduct { CategoryId = 1, ProductId = 5 },
                new CategoryProduct { CategoryId = 1, ProductId = 6 },
                new CategoryProduct { CategoryId = 1, ProductId = 7 },
                new CategoryProduct { CategoryId = 1, ProductId = 8 },
                new CategoryProduct { CategoryId = 2, ProductId = 4 },
                new CategoryProduct { CategoryId = 2, ProductId = 7 },
                new CategoryProduct { CategoryId = 3, ProductId = 7 }
            };
            
            context.CategoryProducts.AddRange(categoryProductSamples);
            await context.SaveChangesAsync();
        }

        if (!await context.Users.AnyAsync())
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            
            var userUser = new User { UserName = "123@gmail.com", Email = "123@gmail.com" };
            var userAdmin = new User { UserName = "admin@gmail.com", Email = "admin@gmail.com" };
            
            await userManager.CreateAsync(userUser, "Suppl2000!");
            await userManager.CreateAsync(userAdmin, "Admin2000!");
        }
    }
}