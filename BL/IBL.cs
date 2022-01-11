namespace BL;

public interface IBL
{
    List<Customer> SearchCustomers(string searchString);

    List<Store> GetAllStores();

    List<Product> GetAllProducts();

    List<Order> GetAllOrders(int Id);

    List<Order> StoreOrders(int storeId);

    void AddCustomer(Customer customerToAdd);

    void AddOrder(int storeId, int productId, string storeName, string productName,  int quantity, decimal price, int userId);

    void UpdateInventory(int productId, int newQuantity); 

    void ReplenishInventory();

    Customer Login(Customer existingCustomer);

    
}