namespace DL;

public interface IRepo
{
    List<Store> GetAllStores();

    List<Product> GetAllProducts();

    List<Order> GetAllOrders(int Id);

    List<Order> StoreOrders(int Id);

    void AddCustomer(Customer customerToAdd);

    void AddOrder(int storeId, int productId, string storeName, string productName, int quantity, decimal price, int userId, DateTime time);

    void UpdateInventory(int productId, int newQuantity);

    void ReplenishInventory();

    Customer Login(Customer existingCustomer);

    List<Customer> SearchCustomers(string searchTerm);

    bool IsDuplicate(Customer customer);
}