using FluentAssertions;
using SupplementsShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SupplementsShop.Domain.Entities;
using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Infrastructure.Tests;

public class OrderRepositoryTests
{
    private readonly DbContextOptions<SupplementsShopContext> _options
        = new DbContextOptionsBuilder<SupplementsShopContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
            .Options;

    [Fact]
    public async Task AddAsync_PersistsOrder()
    {
        var order = new Order(
            firstName: "John",
            lastName: "Smith",
            email: "john.smith@gmail.com",
            phoneNumber: "+49912345678",
            streetAddress1: "Jane Street",
            streetAddress2: null,
            city: "Berlin",
            stateOrRegion: null,
            postalCode: "12345",
            country: "Germany",
            userId: "user-john-1"
            );

        await using (var seedContext = new SupplementsShopContext(_options))
        {
            IOrderRepository repo = new OrderRepository(seedContext);
            await repo.AddAsync(order);
            await seedContext.SaveChangesAsync();
        }

        await using (var seedContext = new SupplementsShopContext(_options))
        {
            IOrderRepository repo = new OrderRepository(seedContext);
            var fetched = await repo.GetByIdAsync(order.Id);
            
            fetched.Should().NotBeNull();
            fetched!.FirstName.Should().Be("John");
            fetched.Items.Should().BeNull();
        }
    }

    [Fact]
    public async Task GetByIdAsync_NonexistentId_ReturnsNull()
    {
        await using var context = new SupplementsShopContext(_options);
        IOrderRepository repo = new OrderRepository(context);
        
        var result = await repo.GetByIdAsync(9999);
        result.Should().BeNull();
        
    }

    [Fact]
    public void GetNextOrderNumber_NoOrders_Returns1000()
    {
        using var context = new SupplementsShopContext(_options);
        IOrderRepository repo = new OrderRepository(context);
        
        var next = repo.GetNextOrderNumber();
        next.Should().Be(1000);
    }

    [Fact]
    public async Task GetNextOrderNumber_WithExistingOrders_ReturnsMaxPlusOne()
    {
        var order1 = new Order(
            firstName: "John",
            lastName: "Smith",
            email: "john.smith@gmail.com",
            phoneNumber: "+49912345678",
            streetAddress1: "Jane Street",
            streetAddress2: null,
            city: "Berlin",
            stateOrRegion: null,
            postalCode: "12345",
            country: "Germany",
            userId: "user-john-1"
        );
        
        var order2 = new Order(
            firstName: "John",
            lastName: "Smith",
            email: "john.smith@gmail.com",
            phoneNumber: "+49912345678",
            streetAddress1: "Jane Street",
            streetAddress2: null,
            city: "Berlin",
            stateOrRegion: null,
            postalCode: "12345",
            country: "Germany",
            userId: "user-john-1"
        );
        
        var order3 =  new Order(
            firstName: "John",
            lastName: "Smith",
            email: "john.smith@gmail.com",
            phoneNumber: "+49912345678",
            streetAddress1: "Jane Street",
            streetAddress2: null,
            city: "Berlin",
            stateOrRegion: null,
            postalCode: "12345",
            country: "Germany",
            userId: "user-john-1"
        );
        
        var orders = new List<Order> { order1, order2, order3 };
        
        await using (var seedContext = new SupplementsShopContext(_options))
        {
            await seedContext.Orders.AddRangeAsync(orders);
            
            var list = seedContext.ChangeTracker.Entries<Order>().ToList();
            list[0].Entity.SetOrderNumber(1005);
            list[1].Entity.SetOrderNumber(1010);
            list[2].Entity.SetOrderNumber(1008);
            
            await seedContext.SaveChangesAsync();
        }
        
        using var testContext = new SupplementsShopContext(_options);
        IOrderRepository repo = new OrderRepository(testContext);
        var next = repo.GetNextOrderNumber();
        
        next.Should().Be(1011);
    }
}