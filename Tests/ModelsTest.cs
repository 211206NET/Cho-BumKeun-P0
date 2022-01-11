using Xunit;
using Models;
using CustomExceptions;
using System.Collections.Generic;

namespace Tests;

public class ModelsTest
{
    [Fact]
    public void UserAccountShouldCreate()
    {
        Customer testCustomer = new Customer();

        Assert.NotNull(testCustomer);
    }

    [Fact]
    public void UserAccountShouldSetValidData()
    {
        Customer testCustomer = new Customer();
        string username = "TestName";
        string password = "TestPassword";

        testCustomer.UserName = username;
        testCustomer.Password = password;

        Assert.Equal(username, testCustomer.UserName);
        Assert.Equal(password, testCustomer.Password);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Test Name")]
    [InlineData("TestName!")]
    [InlineData("TestName1052%#")]
    [InlineData(null)]
    public void UserAccountShouldNotSetInvalidName(string input)
    {
        Customer testCustomer = new Customer();

        Assert.Throws<InputInvalidException>(() => testCustomer.UserName = input);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Test Password")]
    [InlineData("TestPassword1052%#")]
    [InlineData(null)]
    public void UserAccountShouldNotSetInvalidPassword(string input)
    {
        Customer testCustomer = new Customer();

        Assert.Throws<InputInvalidException>(() => testCustomer.Password = input);
    }

    [Fact]
    public void StoreOrdersShouldBeAbleToBeSet()
    {
        Store testStore = new Store();
        List<Order> testOrders = new List<Order>();

        testStore.Orders = testOrders;

        Assert.NotNull(testStore.Orders);
        Assert.Equal(0, testStore.Orders.Count);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(11)]
    [InlineData(null)]
    public void OrderQuantityShouldStayInBounds(int input)
    {
        Order testOrder = new Order();

        Assert.Throws<InputInvalidException>(() => testOrder.Quantity = input);
    }

    [Fact]
    public void StoreShouldHaveCustomToStringMethod()
    {
        Store testStore = new Store
        {
            Id = 1,
            Name = "Test Store",
            City = "Test City",
            State = "Test State"
        };
        string expectedOutput = "Id: 1 \nName: Test Store \nCity: Test City \nState: Test State \n==================================";

        Assert.Equal(expectedOutput, testStore.ToString());
    }

    [Fact]
    public void ProductShouldHaveCustomToStringMethod()
    {
        Product testProduct = new Product
        {
            Id = 1,
            Title = "Test Product",
            Price = 20,
            Developer = "Test Dev",
            Inventory = 100
        };
        string expectedOutput = "Id: 1 \nTitle: Test Product \nDeveloper: Test Dev \nPrice: 20 \nInventory: 100 \n==================================";

        Assert.Equal(expectedOutput, testProduct.ToString());
    }

    [Fact]
    public void OrderShouldHaveCustomToStringMethod()
    {
        Order testOrder = new Order
        {
            Id = 1,
            StoreId = 1,
            StoreName = "Test Store",
            ProductId = 1,
            ProductName = "Test Product",
            Quantity = 4,
            TotalPrice = 60,
            UserId = 1,
            Time = "Test Time"
        };
        string expectedOutput = "Id: 1 \nStore: Test Store \nProduct: Test Product \nQuantity: 4 \nPrice: 60 \nTime: Test Time \n==================================";

        Assert.Equal(expectedOutput, testOrder.ToString());
    }

    [Fact]
    public void CustomerShouldNotSetInvalidName()
    {
        Customer testCustomer = new Customer();
        string invalidName = "!@$%@#%%";

        Assert.Throws<InputInvalidException>(() => testCustomer.UserName = invalidName);
    }
}