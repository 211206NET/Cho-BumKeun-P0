using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
namespace DL;

public class DBRepo : IRepo
{
    private string _connectionString;
    public DBRepo(string connectionString) {
        _connectionString = connectionString;
    }

    /// <summary>
    /// Creates a new customer
    /// </summary>
    /// <param name="customerToAdd">Takes in a customer object to create</param>
    public void AddCustomer(Customer customerToAdd)
    {
        DataSet restoSet = new DataSet();
        string selectCmd = "SELECT * FROM UserAccount WHERE Id = -1";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            using(SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCmd, connection))
            {
                dataAdapter.Fill(restoSet, "UserAccount");

                DataTable restoTable = restoSet.Tables["UserAccount"];
                DataRow newRow = restoTable.NewRow();

                newRow["Username"] = customerToAdd.UserName;
                newRow["Password"] = customerToAdd.Password ?? "";
                
                restoTable.Rows.Add(newRow);

                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dataAdapter);

                dataAdapter.InsertCommand = cmdBuilder.GetInsertCommand();
  
                dataAdapter.Update(restoTable);
            }
        }
    }

    /// <summary>
    /// Adds StoreId, StoreName, ProductId, ProductName, quantity, price, user ID, and time to Order
    /// </summary>
    /// <param name="storeId">Takes in store ID int</param>
    /// <param name="productId">Takes in product ID int</param>
    /// <param name="storeName">Takes in store name string</param>
    /// <param name="productName">Takes in product name string</param>
    /// <param name="quantity">Takes in quantity int</param>
    /// <param name="price">Takes in price decimal</param>
    /// <param name="userId">Takes in user ID int</param>
    /// <param name="time">Takes in DateTime object</param>
    public void AddOrder(int storeId, int productId, string storeName, string productName, int quantity, decimal price, int userId, DateTime time)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Orders (StoreId, StoreName, ProductId, ProductName, Quantity, TotalPrice, UserId, Time) VALUES (@stoId, @stoname, @prodId, @prodname, @quantity, @totalprice, @userId, @time)";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter param = new SqlParameter("@stoId", storeId);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@stoName", storeName);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@prodId", productId);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@prodName", productName);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@quantity", quantity);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@totalprice", price*quantity);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@userId", userId);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@time", DateTime.Now);
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /// <summary>
    /// Updates the inventory count of product
    /// </summary>
    /// <param name="productId">Takes in product ID int to determine which product it will update</param>
    /// <param name="newQuantity">Takes in a new quantity int to set for the specific product</param>
    public void UpdateInventory(int productId, int newQuantity)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE Product SET Inventory = @Quantity WHERE Id = @Id";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter param = new SqlParameter("@Quantity", newQuantity);
                cmd.Parameters.Add(param);
                param = new SqlParameter("@Id", productId);
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /// <summary>
    /// Replenishes all product inventory back to 100
    /// </summary>
    public void ReplenishInventory()
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE Product SET Inventory = 100";
            SqlCommand cmd = new SqlCommand(sqlCmd, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    /// <summary>
    /// Retrieves all the stores in the Store table
    /// </summary>
    /// <returns>A list of all the stores</returns>
    public List<Store> GetAllStores()
    {
        List<Store> allStores = new List<Store>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string stoSelect = "Select * From Store";
        DataSet StoreSet = new DataSet();
        using SqlDataAdapter stoAdapter = new SqlDataAdapter(stoSelect, connection);
        stoAdapter.Fill(StoreSet, "Store");
        DataTable? StoreTable = StoreSet.Tables["Store"];
        if(StoreTable != null)
        {
            foreach(DataRow row in StoreTable.Rows)
            {
                Store sto = new Store(row);
                allStores.Add(sto);
            }
        }
        return allStores;
    }

    /// <summary>
    /// Retrieves all the products in the Product table
    /// </summary>
    /// <returns>A list of all the products</returns>
    public List<Product> GetAllProducts()
    {
        List<Product> allProducts = new List<Product>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string prodSelect = "Select * From Product";
        DataSet ProductSet = new DataSet();
        using SqlDataAdapter prodAdapter = new SqlDataAdapter(prodSelect, connection);
        prodAdapter.Fill(ProductSet, "Product");
        DataTable? ProductTable = ProductSet.Tables["Product"];
        if(ProductTable != null)
        {
            foreach(DataRow row in ProductTable.Rows)
            {
                Product prod = new Product(row);
                allProducts.Add(prod);
            }
        }
        return allProducts;
    }

    /// <summary>
    /// Gets user entire orders
    /// </summary>
    /// <param name="storeId">Takes the user ID to search for orders</param>
    /// <returns>A list of orders for user</returns>
    public List<Order> GetAllOrders(int Id)
    {
        List<Order> allOrders = new List<Order>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string ordSelect = $"Select * From Orders WHERE UserId = {Id}";
        DataSet OrderSet = new DataSet();
        using SqlDataAdapter ordAdapter = new SqlDataAdapter(ordSelect, connection);
        ordAdapter.Fill(OrderSet, "Order");
        DataTable? OrderTable = OrderSet.Tables["Order"];
        if(OrderTable != null)
        {
            foreach(DataRow row in OrderTable.Rows)
            {
                Order ord = new Order(row);
                allOrders.Add(ord);
            }
        }
        return allOrders;
    }

    /// <summary>
    /// Gets user orders sorted by date from old to new
    /// </summary>
    /// <param name="storeId">Takes the user ID to search for orders</param>
    /// <returns>A list of orders for user</returns>
    public List<Order> GetAllOrdersDateON(int Id)
    {
        List<Order> allOrders = new List<Order>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string ordSelect = $"Select * From Orders WHERE UserId = {Id} ORDER BY Time ASC";
        DataSet OrderSet = new DataSet();
        using SqlDataAdapter ordAdapter = new SqlDataAdapter(ordSelect, connection);
        ordAdapter.Fill(OrderSet, "Order");
        DataTable? OrderTable = OrderSet.Tables["Order"];
        if(OrderTable != null)
        {
            foreach(DataRow row in OrderTable.Rows)
            {
                Order ord = new Order(row);
                allOrders.Add(ord);
            }
        }
        return allOrders;
    }

    /// <summary>
    /// Gets user orders sorted by date from new to old
    /// </summary>
    /// <param name="storeId">Takes the user ID to search for orders</param>
    /// <returns>A list of orders for user</returns>
    public List<Order> GetAllOrdersDateNO(int Id)
    {
        List<Order> allOrders = new List<Order>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string ordSelect = $"Select * From Orders WHERE UserId = {Id} ORDER BY Time DESC";
        DataSet OrderSet = new DataSet();
        using SqlDataAdapter ordAdapter = new SqlDataAdapter(ordSelect, connection);
        ordAdapter.Fill(OrderSet, "Order");
        DataTable? OrderTable = OrderSet.Tables["Order"];
        if(OrderTable != null)
        {
            foreach(DataRow row in OrderTable.Rows)
            {
                Order ord = new Order(row);
                allOrders.Add(ord);
            }
        }
        return allOrders;
    }

    /// <summary>
    /// Gets user orders sorted by price from low to high
    /// </summary>
    /// <param name="storeId">Takes the user ID to search for orders</param>
    /// <returns>A list of orders for user</returns>
    public List<Order> GetAllOrdersPriceLH(int Id)
    {
        List<Order> allOrders = new List<Order>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string ordSelect = $"Select * From Orders WHERE UserId = {Id} ORDER BY TotalPrice ASC";
        DataSet OrderSet = new DataSet();
        using SqlDataAdapter ordAdapter = new SqlDataAdapter(ordSelect, connection);
        ordAdapter.Fill(OrderSet, "Order");
        DataTable? OrderTable = OrderSet.Tables["Order"];
        if(OrderTable != null)
        {
            foreach(DataRow row in OrderTable.Rows)
            {
                Order ord = new Order(row);
                allOrders.Add(ord);
            }
        }
        return allOrders;
    }

    /// <summary>
    /// Gets user orders sorted by price from high to low
    /// </summary>
    /// <param name="storeId">Takes the user ID to search for orders</param>
    /// <returns>A list of orders for user</returns>
    public List<Order> GetAllOrdersPriceHL(int Id)
    {
        List<Order> allOrders = new List<Order>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string ordSelect = $"Select * From Orders WHERE UserId = {Id} ORDER BY TotalPrice DESC";
        DataSet OrderSet = new DataSet();
        using SqlDataAdapter ordAdapter = new SqlDataAdapter(ordSelect, connection);
        ordAdapter.Fill(OrderSet, "Order");
        DataTable? OrderTable = OrderSet.Tables["Order"];
        if(OrderTable != null)
        {
            foreach(DataRow row in OrderTable.Rows)
            {
                Order ord = new Order(row);
                allOrders.Add(ord);
            }
        }
        return allOrders;
    }

    /// <summary>
    /// Gets all specified storefront orders
    /// </summary>
    /// <param name="storeId">Takes the store ID to search for orders</param>
    /// <returns>A list of orders for specified store</returns>
    public List<Order> StoreOrders(int storeId)
    {
        List<Order> allOrders = new List<Order>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string ordSelect = $"SELECT * FROM Orders WHERE StoreId = {storeId}";
        DataSet OrderSet = new DataSet();
        using SqlDataAdapter ordAdapter = new SqlDataAdapter(ordSelect, connection);
        ordAdapter.Fill(OrderSet, "Order");
        DataTable? OrderTable = OrderSet.Tables["Order"];
        if(OrderTable!= null)
        {
            foreach(DataRow row in OrderTable.Rows)
            {
                Order ord = new Order(row);
                allOrders.Add(ord);
            }
        }
        return allOrders;
    }

    /// <summary>
    /// Gets specified storefront orders sorted by date from old to new
    /// </summary>
    /// <param name="storeId">Takes the store ID to search for orders</param>
    /// <returns>A list of orders for specified store</returns>
    public List<Order> GetAllOrdersStoreDateON(int storeId)
    {
        List<Order> allOrders = new List<Order>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string ordSelect = $"SELECT * FROM Orders WHERE StoreId = {storeId} ORDER BY Time ASC";
        DataSet OrderSet = new DataSet();
        using SqlDataAdapter ordAdapter = new SqlDataAdapter(ordSelect, connection);
        ordAdapter.Fill(OrderSet, "Order");
        DataTable? OrderTable = OrderSet.Tables["Order"];
        if(OrderTable!= null)
        {
            foreach(DataRow row in OrderTable.Rows)
            {
                Order ord = new Order(row);
                allOrders.Add(ord);
            }
        }
        return allOrders;
    }

    /// <summary>
    /// Gets specified storefront orders sorted by date from new to old
    /// </summary>
    /// <param name="storeId">Takes the store ID to search for orders</param>
    /// <returns>A list of orders for specified store</returns>
    public List<Order> GetAllOrdersStoreDateNO(int storeId)
    {
        List<Order> allOrders = new List<Order>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string ordSelect = $"SELECT * FROM Orders WHERE StoreId = {storeId} ORDER BY Time DESC";
        DataSet OrderSet = new DataSet();
        using SqlDataAdapter ordAdapter = new SqlDataAdapter(ordSelect, connection);
        ordAdapter.Fill(OrderSet, "Order");
        DataTable? OrderTable = OrderSet.Tables["Order"];
        if(OrderTable!= null)
        {
            foreach(DataRow row in OrderTable.Rows)
            {
                Order ord = new Order(row);
                allOrders.Add(ord);
            }
        }
        return allOrders;
    }

    /// <summary>
    /// Gets specified storefront orders sorted by price from low to high
    /// </summary>
    /// <param name="storeId">Takes the store ID to search for orders</param>
    /// <returns>A list of orders for specified store</returns>
    public List<Order> GetAllOrdersStorePriceLH(int storeId)
    {
        List<Order> allOrders = new List<Order>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string ordSelect = $"SELECT * FROM Orders WHERE StoreId = {storeId} ORDER BY TotalPrice ASC";
        DataSet OrderSet = new DataSet();
        using SqlDataAdapter ordAdapter = new SqlDataAdapter(ordSelect, connection);
        ordAdapter.Fill(OrderSet, "Order");
        DataTable? OrderTable = OrderSet.Tables["Order"];
        if(OrderTable!= null)
        {
            foreach(DataRow row in OrderTable.Rows)
            {
                Order ord = new Order(row);
                allOrders.Add(ord);
            }
        }
        return allOrders;
    }

    /// <summary>
    /// Gets specified storefront orders sorted by price from high to low
    /// </summary>
    /// <param name="storeId">Takes the store ID to search for orders</param>
    /// <returns>A list of orders for specified store</returns>
    public List<Order> GetAllOrdersStorePriceHL(int storeId)
    {
        List<Order> allOrders = new List<Order>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string ordSelect = $"SELECT * FROM Orders WHERE StoreId = {storeId} ORDER BY TotalPrice DESC";
        DataSet OrderSet = new DataSet();
        using SqlDataAdapter ordAdapter = new SqlDataAdapter(ordSelect, connection);
        ordAdapter.Fill(OrderSet, "Order");
        DataTable? OrderTable = OrderSet.Tables["Order"];
        if(OrderTable!= null)
        {
            foreach(DataRow row in OrderTable.Rows)
            {
                Order ord = new Order(row);
                allOrders.Add(ord);
            }
        }
        return allOrders;
    }

    /// <summary>
    /// Search for the Username for exact match of name
    /// </summary>
    /// <param name="customer">Customer object to search for dup</param>
    /// <returns>bool: true if there is duplicate, false if not</returns>
    public bool IsDuplicate(Customer customer)
    {
        string searchQuery = $"SELECT * FROM UserAccount WHERE Username='{customer.UserName}'";
        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand(searchQuery, connection);
        connection.Open();
        using SqlDataReader reader = cmd.ExecuteReader();
        if(reader.HasRows)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Retrieves the row with Id, UserName, Password in the UserAccount table
    /// </summary>
    /// <param name="customer">Customer object to search for match</param>
    /// <returns>A customer object by the logged in account</returns>
    public Customer Login(Customer customer)
    {
        string searchQuery = $"SELECT * FROM UserAccount WHERE Username='{customer.UserName}'";
        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand(searchQuery, connection);
        connection.Open();
        using SqlDataReader reader = cmd.ExecuteReader();
        Customer acc = new Customer();
        if(reader.Read())
        {
            acc.Id = reader.GetInt32(0);
            acc.UserName = reader.GetString(1);
            acc.Password = reader.GetString(2);
        }
        return acc;
    }
}