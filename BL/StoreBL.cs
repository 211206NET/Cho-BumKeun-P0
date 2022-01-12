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
    /// Gets all stores
    /// </summary>
    /// <returns>List of all stores</returns>
    public List<Store> GetAllStores()
    {
        return _dl.GetAllStores();
    }

    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns>List of all products</returns>
    public List<Product> GetAllProducts()
    {
        return _dl.GetAllProducts();
    }

    /// <summary>
    /// Get all orders by user ID
    /// </summary>
    /// <param name="Id">Takes in user ID as int</param>
    /// <returns>List of all orders for user</returns>
    public List<Order> GetAllOrders(int Id)
    {
        return _dl.GetAllOrders(Id);
    }

    /// <summary>
    /// Get all orders by user ID for sorting by time old to new
    /// </summary>
    /// <param name="Id">Takes in user ID as int</param>
    /// <returns>List of all orders for user sorted time old to new</returns>
    public List<Order> GetAllOrdersDateON(int Id)
    {
        return _dl.GetAllOrdersDateON(Id);
    }

    /// <summary>
    /// Get all orders by user ID for sorting by time new to old
    /// </summary>
    /// <param name="Id">Takes in user ID as int</param>
    /// <returns>List of all orders for user sorted time new to old</returns>
    public List<Order> GetAllOrdersDateNO(int Id)
    {
        return _dl.GetAllOrdersDateNO(Id);
    }

    /// <summary>
    /// Get all orders by user ID for sorting by price low to high
    /// </summary>
    /// <param name="Id">Takes in user ID as int</param>
    /// <returns>List of all orders for user sorted price low to high</returns>
    public List<Order> GetAllOrdersPriceLH(int Id)
    {
        return _dl.GetAllOrdersPriceLH(Id);
    }

    /// <summary>
    /// Get all orders by user ID for sorting by price high to low
    /// </summary>
    /// <param name="Id">Takes in user ID as int</param>
    /// <returns>List of all orders for user sorted high price to low</returns>
    public List<Order> GetAllOrdersPriceHL(int Id)
    {
        return _dl.GetAllOrdersPriceHL(Id);
    }
    
    /// <summary>
    /// Get all orders for a specific store by store ID
    /// </summary>
    /// <param name="Id">Takes in store ID as int</param>
    /// <returns>List of all orders for specific store</returns>
    public List<Order> StoreOrders(int Id)
    {
        return _dl.StoreOrders(Id);
    }

    /// <summary>
    /// Get all sorted orders by time old to new for a specific store by store ID
    /// </summary>
    /// <param name="Id">Takes in store ID as int</param>
    /// <returns>List of all orders sorted by time old to new for specific store</returns>
    public List<Order> GetAllOrdersStoreDateON(int Id)
    {
        return _dl.GetAllOrdersStoreDateON(Id);
    }

    /// <summary>
    /// Get all sorted orders by time new to old for a specific store by store ID
    /// </summary>
    /// <param name="Id">Takes in store ID as int</param>
    /// <returns>List of all orders sorted by time new to old for specific store</returns>
    public List<Order> GetAllOrdersStoreDateNO(int Id)
    {
        return _dl.GetAllOrdersStoreDateNO(Id);
    }

    /// <summary>
    /// Get all sorted orders by price low to high for a specific store by store ID
    /// </summary>
    /// <param name="Id">Takes in store ID as int</param>
    /// <returns>List of all orders sorted by price low to high for specific store</returns>
    public List<Order> GetAllOrdersStorePriceLH(int Id)
    {
        return _dl.GetAllOrdersStorePriceLH(Id);
    }

    /// <summary>
    /// Get all sorted orders by price high to low for a specific store by store ID
    /// </summary>
    /// <param name="Id">Takes in store ID as int</param>
    /// <returns>List of all orders sorted by price high to low for specific store</returns>
    public List<Order> GetAllOrdersStorePriceHL(int Id)
    {
        return _dl.GetAllOrdersStorePriceHL(Id);
    }

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

    /// <summary>
    /// Takes the user input and returns matching Customer object
    /// </summary>
    /// <param name="checkCustomer">Takes customer object of inputted strings</param>
    /// <returns>Customer object matching inputted strings</returns>
    public Customer Login(Customer checkCustomer)
    {
        return _dl.Login(checkCustomer);
    }

    /// <summary>
    /// Takes the values to place into the Order table
    /// </summary>
    /// <param name="storeId">Selected store ID</param>
    /// <param name="productId">Selected product ID</param>
    /// <param name="storeName">Name of the store</param>
    /// <param name="productName">Name of the product</param>
    /// <param name="quantity">Amount purchasing</param>
    /// <param name="price">Price of purchase</param>
    /// <param name="userId">User ID of customer</param>
    /// <param name="time">Current time of purchase</param>
    public void AddOrder(int storeId, int productId, string storeName, string productName, int quantity, decimal price, int userId, DateTime time)
    {
        _dl.AddOrder(storeId, productId, storeName, productName, quantity, price, userId, time);
    }

    /// <summary>
    /// Updates the inventory amount for products
    /// </summary>
    /// <param name="productId">Selected product ID</param>
    /// <param name="newQuantity">The difference of inventory and quantity purchased</param>
    public void UpdateInventory(int productId, int newQuantity)
    {
        _dl.UpdateInventory(productId, newQuantity);
    }

    public void ReplenishInventory()
    {
        _dl.ReplenishInventory();
    }
}