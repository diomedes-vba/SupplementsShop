using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;
using SupplementsShop.Infrastructure.Persistence;

namespace SupplementsShop.Infrastructure.Tests;

public class CartItemRepositoryTests
{
    private readonly DbContextOptions<SupplementsShopContext> _options
        = new DbContextOptionsBuilder<SupplementsShopContext>()
            .UseInMemoryDatabase(databaseName: $"CartItemTestDb_{Guid.NewGuid()}")
            .Options;

    [Fact]
    public async Task AddToCartAsync_PersistsNewItem()
    {
        var cartItem = new CartItemContext(productId: 7, quantity: 1, userId: "u1");

        await using (var seedContext = new SupplementsShopContext(_options))
        {
            ICartItemRepository repo = new CartItemRepository(seedContext);
            await repo.AddToCartAsync(cartItem);
        }

        await using (var seedContext = new SupplementsShopContext(_options))
        {
            seedContext.CartItems.Should().ContainSingle(ci => 
                ci.UserId == "u1" &&
                ci.ProductId == 7 &&
                ci.Quantity == 1);
        }
    }

    [Fact]
    public async Task GetCartItemAsync_ReturnsCorrectItemOrNull()
    {
        var itemA = new CartItemContext(5, 2, "A");
        await using (var seedContext = new SupplementsShopContext(_options))
        {
            seedContext.CartItems.Add(itemA);
            await seedContext.SaveChangesAsync();
        }

        await using (var seedContext = new SupplementsShopContext(_options))
        {
            ICartItemRepository repo = new CartItemRepository(seedContext);
            var fetched = await repo.GetCartItemAsync("A", 5);
            fetched.Should().NotBeNull()
                .And.Match<CartItemContext>(ci => ci.Quantity == 2);
            
            var missing1 = await repo.GetCartItemAsync("A", 999);
            var missing2 = await repo.GetCartItemAsync("B", 5);
            
            missing1.Should().BeNull();
            missing2.Should().BeNull();
        }
    }

    [Fact]
    public async Task GetCartItemListForUserAsync_FiltersByUserId()
    {
        var items = new[]
        {
            new CartItemContext(1, 1, "User"),
            new CartItemContext(2, 3, "User"),
            new CartItemContext(1, 2, "OtherUser")
        };
        var products = new[]
        {
            new Product("Product1", "prdNmb", 30, " ", 1, "prdSlug", "prdImg"),
            new Product("Product2", "prdNmb", 25, " ", 1, "prdSlug", "prdImg")
        };
        await using (var seedContext = new SupplementsShopContext(_options))
        {
            seedContext.CartItems.AddRange(items);
            seedContext.Products.AddRange(products);
            await seedContext.SaveChangesAsync();
        }

        await using (var seedContext = new SupplementsShopContext(_options))
        {
            ICartItemRepository repo = new CartItemRepository(seedContext);
            var list = await repo.GetCartItemListForUserAsync("User");
            
            list.Should().HaveCount(2)
                .And.OnlyContain(ci => ci.UserId == "User");
        }
    }

    [Fact]
    public async Task GetCartItemIdAsync_ReturnsCorrectItemIdOrNull()
    {
        var ci = new CartItemContext(8, 4, "user8");
        await using (var seedContext = new SupplementsShopContext(_options))
        {
            await seedContext.CartItems.AddAsync(ci);
            await seedContext.SaveChangesAsync();
        }

        await using (var seedContext = new SupplementsShopContext(_options))
        {
            ICartItemRepository repo = new CartItemRepository(seedContext);
            var id = await repo.GetCartItemIdAsync(8, "user8");
            id.Should().Be(ci.Id);
            
            var none = await repo.GetCartItemIdAsync(999, "user8");
            none.Should().BeNull();
        }
    }
}