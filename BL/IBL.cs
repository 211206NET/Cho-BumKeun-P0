namespace BL;

public interface IBL
{
    List<Customer> SearchCustomers(string searchString);

    List<Store> GetAllStores();

    List<Product> GetAllProducts();

    List<Order> GetAllOrders(int Id);

    List<Order> GetAllOrdersDateON(int Id);

    List<Order> GetAllOrdersDateNO(int Id);

    List<Order> GetAllOrdersPriceLH(int Id);

    List<Order> GetAllOrdersPriceHL(int Id);

    List<Order> StoreOrders(int storeId);

    List<Order> GetAllOrdersStoreDateON(int storeId);

    List<Order> GetAllOrdersStoreDateNO(int storeId);

    List<Order> GetAllOrdersStorePriceLH(int storeId);

    List<Order> GetAllOrdersStorePriceHL(int storeId);

    void AddCustomer(Customer customerToAdd);

    void AddOrder(int storeId, int productId, string storeName, string productName,  int quantity, decimal price, int userId, DateTime time);

    void UpdateInventory(int productId, int newQuantity); 

    void ReplenishInventory();

    Customer Login(Customer existingCustomer);

    
}