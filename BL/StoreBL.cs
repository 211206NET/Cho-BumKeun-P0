using CustomExceptions;
namespace BL;

public class StoreBL : IBL
{
    private IRepo _dl;

    public StoreBL(IRepo repo)
    {
        _dl = repo;
    }

    /// <summary>
    /// Gets all restaurants
    /// </summary>
    /// <returns>list of all restaurants</returns>
    public List<Store> GetAllStores()
    {
        return _dl.GetAllStores();
    }

    public List<Product> GetAllProducts()
    {
        return _dl.GetAllProducts();
    }

    public List<Order> GetAllOrders(int Id)
    {
        return _dl.GetAllOrders(Id);
    }

    public List<Order> GetAllOrdersDateON(int Id)
    {
        return _dl.GetAllOrdersDateON(Id);
    }

    public List<Order> GetAllOrdersDateNO(int Id)
    {
        return _dl.GetAllOrdersDateNO(Id);
    }

    public List<Order> GetAllOrdersPriceLH(int Id)
    {
        return _dl.GetAllOrdersPriceLH(Id);
    }

    public List<Order> GetAllOrdersPriceHL(int Id)
    {
        return _dl.GetAllOrdersPriceHL(Id);
    }
    
    public List<Order> StoreOrders(int Id)
    {
        return _dl.StoreOrders(Id);
    }

    public List<Order> GetAllOrdersStoreDateON(int Id)
    {
        return _dl.GetAllOrdersStoreDateON(Id);
    }

    public List<Order> GetAllOrdersStoreDateNO(int Id)
    {
        return _dl.GetAllOrdersStoreDateNO(Id);
    }

    public List<Order> GetAllOrdersStorePriceLH(int Id)
    {
        return _dl.GetAllOrdersStorePriceLH(Id);
    }

    public List<Order> GetAllOrdersStorePriceHL(int Id)
    {
        return _dl.GetAllOrdersStorePriceHL(Id);
    }

    /// <summary>
    /// Adds a new restaurant to the list
    /// </summary>
    /// <param name="restaurantToAdd">restaurant object to add</param>
    // public void AddRestaurant(Restaurant restaurantToAdd)
    // {
    //     _dl.AddRestaurant(restaurantToAdd);
    // }

    /// <summary>
    /// Adds a new review to the restaurant that exists on that index
    /// </summary>
    /// <param name="restaurantId">index of the restaurant to leave a review for</param>
    /// <param name="reviewToAdd">a review object to be added to the restaurant</param>
    // public void AddReview(int restaurantId, Review reviewToAdd)
    // {
    //     _dl.AddReview(restaurantId, reviewToAdd);
    // }

    /// <summary>
    /// Adds a new customer to the list
    /// </summary>
    /// <param name="customerToAdd">customer object to add</param>
    public void AddCustomer(Customer customerToAdd)
    {
        if (!_dl.IsDuplicate(customerToAdd))
        {
            _dl.AddCustomer(customerToAdd);
        }
        else throw new DuplicateRecordException("That username is taken");
    }

    public Customer Login(Customer checkCustomer)
    {
        return _dl.Login(checkCustomer);
    }

    public void AddOrder(int storeId, int productId, string storeName, string productName, int quantity, decimal price, int userId, DateTime time)
    {
        _dl.AddOrder(storeId, productId, storeName, productName, quantity, price, userId, time);
    }

    public void UpdateInventory(int productId, int newQuantity)
    {
        _dl.UpdateInventory(productId, newQuantity);
    }

    public void ReplenishInventory()
    {
        _dl.ReplenishInventory();
    }

    public List<Customer> SearchCustomers(string searchTerm)
    {
        return _dl.SearchCustomers(searchTerm);
    }
}