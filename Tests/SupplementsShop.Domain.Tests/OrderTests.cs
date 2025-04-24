using FluentAssertions;
using SupplementsShop.Domain.Entities;

namespace SupplementsShop.Domain.Tests;

public class OrderTests
{
    [Fact]
    public void Ctor_SetsCustomerAndAddressProperties()
    {
        var firstName = "John";
        var lastName = "Smith";
        var email = "john.smith@gmail.com";
        var phoneNumber = "+49912345678";
        var address1 = "Jane Street";
        string? address2 = null;
        var city = "Berlin";
        string? region = null;
        var postalCode = "12345";
        var country = "Germany";
        var userId = "user-john-1";

        var order = new Order(
            firstName, lastName, email, phoneNumber, address1, address2, city, region, postalCode, country, userId);
        
        order.FirstName.Should().Be(firstName);
        order.LastName.Should().Be(lastName);
        order.Email.Should().Be(email);
        order.PhoneNumber.Should().Be(phoneNumber);
        order.StreetAddress1.Should().Be(address1);
        order.StreetAddress2.Should().BeNull();
        order.City.Should().Be(city);
        order.StateOrRegion.Should().BeNull();
        order.PostalCode.Should().Be(postalCode);
        order.Country.Should().Be(country);
        order.UserId.Should().Be(userId);

        order.Items.Should().BeNull();
        order.OrderNumber.Should().Be(0);
        order.OrderDate.Should().Be(default);
    }

    [Fact]
    public void AddItems_SetsItemsCollection()
    {
        var items = new List<OrderItem>
        {
            new OrderItem("Item1", 1.99m, 1, "someurl/someimage1.jpg", 5, "100-000"),
            new OrderItem("Item2", 3.99m, 2, "someurl/someimage2.jpg", 6, "100-001")
        };
        var order = new Order();
        
        order.AddItems(items);
        
        order.Items.Should().BeSameAs(items);
    }

    [Fact]
    public void SetOrderNumber_SetsTheOrderNumber()
    {
        var order = new Order();
        
        order.SetOrderNumber(12345);
        
        order.OrderNumber.Should().Be(12345);
    }

    [Fact]
    public void SetOrderDate_SetsTheOrderDate()
    {
        var order = new Order();
        var dateTimeNow = DateTime.Now;
        order.SetOrderDate(dateTimeNow);
        
        order.OrderDate.Should().Be(dateTimeNow);
    }
}